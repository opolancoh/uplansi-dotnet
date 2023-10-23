using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Uplansi.Core.Entities.Account;
using Uplansi.Data.EntityFramework;

namespace Uplansi.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services)
    {
        var appDbConnection = Environment.GetEnvironmentVariable("UPLANSI_DB_CONNECTION");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(appDbConnection);
            options.EnableSensitiveDataLogging();
        });
        services.AddDbContext<AccountDbContext>(options =>
        {
            options.UseNpgsql(appDbConnection);
            options.EnableSensitiveDataLogging();
        });
        // services.AddScoped<IEmployeeEntityFrameworkRepository, EmployeeEntityFrameworkRepository>();
        // services.AddScoped<IEmployeeEntityFrameworkService, EmployeeEntityFrameworkService>();

    }
    
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<AccountDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        });
    }
}
