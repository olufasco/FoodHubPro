using FoodHubPro.Application.Interfaces;
using FoodHubPro.Domain.Entities;
using FoodHubPro.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FoodHubPro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<SignInManager<ApplicationUser>>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        // Register DbContext using MySQL
        services.AddDbContext<FoodHubDbContext>(options =>
        {
            var cs = configuration.GetConnectionString("DefaultConnection");

            options.UseMySql(cs, new MySqlServerVersion(new Version(11, 8, 0)));
        });

        // Register Identity
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
        })
        .AddRoles<IdentityRole<Guid>>() // adds role support
        .AddEntityFrameworkStores<FoodHubDbContext>()
        .AddSignInManager<SignInManager<ApplicationUser>>() // adds sign-in manager
        .AddDefaultTokenProviders();

        return services;
    }
}
