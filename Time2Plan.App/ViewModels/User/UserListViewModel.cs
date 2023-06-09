﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Time2Plan.App.Messages;
using Time2Plan.App.Services;
using Time2Plan.BL.Facades;
using Time2Plan.BL.Models;

namespace Time2Plan.App.ViewModels;

public partial class UserListViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _UserFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<UserListModel> Users { get; set; } = null!;
    private AppShellViewModel _viewModel;

    public UserListViewModel(
        IUserFacade UserFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        _UserFacade = UserFacade;
        _navigationService = navigationService;
        _viewModel = (AppShellViewModel)Shell.Current.BindingContext;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await _UserFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToActivityListAsync(Guid Id)
    {
        _viewModel.UserId = Id;
        MessengerService.Send(new UserChangeMessage { UserId = Id });
        await _navigationService.GoToAsync("//Activities");
    }


    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await _navigationService.GoToAsync("/edit");
    }

    public async void Receive(UserEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task GoToRefreshAsync()
    {
        await LoadDataAsync();
    }
}
