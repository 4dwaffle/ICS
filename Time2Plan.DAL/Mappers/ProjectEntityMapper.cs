﻿using Time2Plan.DAL.Entities;

namespace Time2Plan.DAL.Mappers;

public class ProjectEntityMapper : IEntityMapper<ProjectEntity>
{
    public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity)
    {

        existingEntity.Description = newEntity.Description;
        existingEntity.Name = newEntity.Name;
    }
}
