﻿using Microsoft.EntityFrameworkCore;
using Time2Plan.DAL.Entities;

namespace Time2Plan.Common.Tests.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity EmptyProject = new()
    {
        Id = default,
        Name = default!,
        Description = default,
    };

    public static readonly ProjectEntity ProjectAlpha = new()
    {
        Id = Guid.NewGuid(),
        Name = "Project Alpha",
        Description = "A software development project"
    };

    public static readonly ProjectEntity ProjectBeta = new()
    {
        Id = Guid.NewGuid(),
        Name = "Project Beta",
        Description = "A marketing campaign"
    };

    public static readonly ProjectEntity ProjectWithActivities = new()
    {
        Id = Guid.NewGuid(),
        Name = "Pojrect with activities",
        Description = "Coding and running"
    };

    public static readonly ProjectEntity ProjectAlphaDelete = ProjectAlpha with { Id = Guid.NewGuid() };
    public static readonly ProjectEntity ProjectAlphaUpdate = ProjectAlpha with { Id = Guid.NewGuid() };
    public static readonly ProjectEntity ProjectWithActivitiesDelete = ProjectWithActivities with { Id = Guid.NewGuid() };
    public static readonly ProjectEntity ProjectWithActivitiesUpdate = ProjectWithActivities with { Id = Guid.NewGuid() };


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectAlpha,
            ProjectBeta,
            ProjectAlphaDelete,
            ProjectAlphaUpdate,
            ProjectWithActivities,
            ProjectWithActivitiesUpdate,
            ProjectWithActivitiesDelete);
    }
}

