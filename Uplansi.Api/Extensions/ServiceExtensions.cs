using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Uplansi.Core.Contracts.Repositories;
using Uplansi.Core.Contracts.Services;
using Uplansi.Core.Entities.Account;
using Uplansi.Data.EntityFramework;
using Uplansi.Services.Data.v1;

namespace Uplansi.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services)
    {
        var appDbConnection = Environment.GetEnvironmentVariable("UPLANSI_DB_CONNECTION");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(appDbConnection).LogTo(Console.WriteLine, LogLevel.Information);
            options.EnableSensitiveDataLogging();
        });
        services.AddDbContext<AccountDbContext>(options =>
        {
            options.UseNpgsql(appDbConnection).LogTo(Console.WriteLine, LogLevel.Information);
            options.EnableSensitiveDataLogging();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
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
    
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
    }
    
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                );
            }
        );
    }
}
