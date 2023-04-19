﻿using Microsoft.EntityFrameworkCore;

namespace Time2Plan.DAL.Factories;

public class SqlServerDbContextFactory : IDbContextFactory<Time2PlanDbContext>
{
    private readonly bool _seedDemoData;
    private readonly DbContextOptionsBuilder<Time2PlanDbContext> _contextOptionsBuilder = new();

    public SqlServerDbContextFactory(string connectionString, bool seedDemoData = false)
    {
        _seedDemoData = seedDemoData;

        _contextOptionsBuilder.UseSqlServer(connectionString);
    }

    public Time2PlanDbContext CreateDbContext() => new(_contextOptionsBuilder.Options, _seedDemoData);
}
