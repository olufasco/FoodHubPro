using FoodHubPro.Domain.Entities;
using FoodHubPro.Infrastructure; // where FoodHubDbContext lives
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;

public class FoodHubDbContextFactory : IDesignTimeDbContextFactory<FoodHubDbContext>
{
    public FoodHubDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var cs = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<FoodHubDbContext>();
        optionsBuilder.UseMySql(
            cs, new MySqlServerVersion(new Version(11, 8, 0))
        );

        return new FoodHubDbContext(optionsBuilder.Options);
    }
}
