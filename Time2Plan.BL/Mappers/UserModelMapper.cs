﻿using Time2Plan.BL.Models;
using Time2Plan.DAL.Entities;

namespace Time2Plan.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>,
    IUserModelMapper
{
    private readonly IUserProjectModelMapper? _userProjectModelMapper;
    private readonly IActivityModelMapper? _activityModelMapper;

    public UserModelMapper(IUserProjectModelMapper userProjectModelMapper, IActivityModelMapper activityModelMapper)
    {
        _userProjectModelMapper = userProjectModelMapper;
        _activityModelMapper = activityModelMapper;
    }

    public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                NickName = entity.NickName,
                Photo = entity.Photo
            };

    public override UserDetailModel MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                NickName = entity.NickName,
                Photo = entity.Photo,
                UserProjects = _userProjectModelMapper!.MapToListModel(entity.UserProjects).ToObservableCollection()
            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Surname = model.Surname,
            NickName = model.NickName,
            Photo = model.Photo
        };
}
