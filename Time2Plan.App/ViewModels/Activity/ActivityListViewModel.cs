﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Time2Plan.App.Messages;
using Time2Plan.App.Services;
using Time2Plan.BL.Facades;
using Time2Plan.BL.Models;
using static Time2Plan.BL.Facades.IActivityFacade;

namespace Time2Plan.App.ViewModels;

public partial class ActivityListViewModel : ViewModelBase, IRecipient<ActivityEditMessage>, IRecipient<ActivityDeleteMessage>, IRecipient<UserChangeMessage>, IRecipient<ProjectDeleteMessage>, IRecipient<ProjectLeaveMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    public Guid UserId { get; set; }

    public string[] Filters { get; set; } = Enum.GetNames(typeof(Interval));

    public string SelectedFilter { get; set; } = Enum.GetName<Interval>(Interval.All);

    public Interval Interval { get; set; }

    public DateTime? FilterStart { get; set; } = DateTime.Now;

    public DateTime? FilterEnd { get; set; } = DateTime.Now;

    public bool ManualFilter { get; set; }

    public ActivityListViewModel(
       IActivityFacade activityFacade,
       INavigationService navigationService,
       IMessengerService messengerService)
       : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        var viewModel = (AppShellViewModel)Shell.Current.BindingContext;
        UserId = viewModel.UserId;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activities = await _activityFacade.GetAsyncListByUser(UserId);
        ManualFilter = false;
        ParseInterval(SelectedFilter);

        if (FilterEnd == null)
        {
            FilterEnd = GetMaxTime(Activities, FilterEnd);
        }
        if (FilterStart == null)
        {
            FilterStart = GetMinTime(Activities, FilterStart);
        }

        Activities = await _activityFacade.GetAsyncFilter(UserId, FilterStart, FilterEnd, null, null); //TODO
    }

    private void ParseInterval(string selectedFilter)
    {
        if (selectedFilter == null)
            return;

        Interval = (Interval)Enum.Parse(typeof(Interval), SelectedFilter);

        DateTime now = DateTime.Now;
        switch (Interval)
        {
            case Interval.Daily:
                FilterStart = now.AddDays(-1);
                break;
            case Interval.Weekly:
                FilterStart = now.AddDays(-7);
                break;
            case Interval.This_Month:
                FilterStart = DateTime.MinValue.AddYears(now.Year - 1).AddMonths(now.Month - 1); //first day of month
                break;
            case Interval.Last_Month:
                FilterStart = DateTime.MinValue.AddYears(now.Year - 1).AddMonths(now.Month - 2); //first day of month
                FilterEnd = DateTime.MinValue.AddYears(now.Year - 1).AddMonths(now.Month - 1).AddDays(-1); //last day of month
                return;
            case Interval.Yearly:
                FilterStart = now.AddYears(-1);
                break;
            case Interval.All:
                FilterStart = GetMinTime(Activities, FilterStart);
                FilterEnd = GetMaxTime(Activities, FilterEnd);
                return;
            case Interval.Manual:
                ManualFilter = true;
                return;
            default:
                throw new Exception("Undefined interval");
        }
        FilterEnd = now;
    }

    [RelayCommand]
    private async Task GoToCreateAsync(Guid userId)
    {
        await _navigationService.GoToAsync($"/edit?userId={userId}");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await _navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object> { [nameof(ActivityDetailViewModel.Id)] = id });
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task GoToRefreshAsync()
    {
        await LoadDataAsync();
    }

    public async void Receive(UserChangeMessage message)
    {
        UserId = message.UserId;
        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public static DateTime? GetMinTime(IEnumerable<ActivityListModel> userActivities, DateTime? Start)
    {
        foreach (var userActivity in userActivities)
        {
            if (userActivity.Start < Start)
            {
                Start = userActivity.Start;
            }
        }
        return Start;
    }
    public static DateTime? GetMaxTime(IEnumerable<ActivityListModel> userActivities, DateTime? End)
    {
        foreach (var userActivity in userActivities)
        {
            if (userActivity.End > End)
            {
                End = userActivity.End;
            }
        }
        return End;
    }

    public async void DatePicker_PropertyChanged(object sender, SelectionChangedEventArgs e)
    {
        await GoToRefreshAsync();
    }

    public async void Receive(ProjectLeaveMessage message)
    {
        foreach (var userActivity in Activities)
        {
            if (userActivity.ProjectId == message.ProjectId)
            {
                await _activityFacade.DeleteAsync(userActivity.Id);
                MessengerService.Send(new ActivityDeleteMessage());
            }
        }
    }
}
