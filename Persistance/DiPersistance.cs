using Application.Entities;
using Application.Interfaces;
using Application.Interfaces.Identity.Services;

using Infrastructure.Identity.Services;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

using Serilog;

namespace Persistance;
public static class DiPersistance
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Information("AddPersistance method started");

        // Register AppDbContext
        services.AddDbContextFactory<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AppDbContextConnection")));
        services.AddScoped<IAppDbContext>(provider =>
        {
            var factory = provider.GetRequiredService<IDbContextFactory<AppDbContext>>();
            return factory.CreateDbContext();
        });

        // Register AsDbContext
        //services.AddDbContextFactory<AsDbContextFactory>(provider => new AsDbContextFactory(configuration));

        services.AddDbContextFactory<AsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AsDbContextConnection")));
        services.AddScoped<IAsDbContext>(provider =>
        {
            var factory = provider.GetRequiredService<IDbContextFactory<AsDbContext>>();
            return factory.CreateDbContext();
        });

        // Register BNPDbContext
        services.AddDbContextFactory<BNPDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BNPDbContextConnection")));
        services.AddScoped<IBNPDbContext>(provider =>
        {
            var factory = provider.GetRequiredService<IDbContextFactory<BNPDbContext>>();
            return factory.CreateDbContext();
        });

        // Register AutoStacjaDbContext
        services.AddDbContextFactory<AutoStacjaDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AutoStacjaDbContextConnection")));
        services.AddScoped<IAutoStacjaDbContext>(provider =>
        {
            var factory = provider.GetRequiredService<IDbContextFactory<AutoStacjaDbContext>>();
            return factory.CreateDbContext();
        });


        services.AddScoped<ITokenValidatedHandlerService, TokenValidatedHandlerService>();

        services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();


        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(configuration.GetSection("AzureAd"));

        services.AddControllersWithViews()
            .AddMicrosoftIdentityUI();

        services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
        {
            options.SignedOutCallbackPath = new PathString("/signout-oidc");
            options.SignedOutRedirectUri = "/";
            options.CallbackPath = "/login-callback";
            options.Events = new OpenIdConnectEvents
            {
                OnTokenValidated = async context =>
                {
                    //var tokenValidatedHandler = context.HttpContext
                    //.RequestServices
                    //.GetRequiredService<ITokenValidatedHandlerService>();

                    //await tokenValidatedHandler.HandleTokenValidation(context);


                    try
                    {
                        var tokenValidatedHandler = context.HttpContext
                            .RequestServices
                            .GetRequiredService<ITokenValidatedHandlerService>();

                        await tokenValidatedHandler.HandleTokenValidation(context);
                        Console.WriteLine("Token validation handled successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during token validation: {ex.Message}");
                    }




                }

            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Dispo", policy => policy.RequireRole("Disposition", "DispositionManager", "Technician", "Administrator", "AppAdmin"));
            options.AddPolicy("Compliance Assistant", policy => policy
            .RequireRole("ComplianceAssistant", "Technician", "Administrator", "AppAdmin"));

            options.AddPolicy("Compliance", policy => policy
            .RequireRole("Compliance", "Technician", "Administrator", "AppAdmin"));

            options.AddPolicy("Accountant", policy => policy
            .RequireRole("Accountant", "AccountantTL", "Technician", "Administrator", "AppAdmin"));

            options.AddPolicy("Settlements", policy => policy
            .RequireRole("Settlement", "Technician", "Administrator", "AppAdmin"));
            options.AddPolicy("ManagerSettlement", policy => policy.RequireRole("Manager", "Technician", "Administrator", "AppAdmin"));

            options.AddPolicy("User", policy => policy
            .RequireRole("User", "Accountant", "AccountantTL", "Settlement", "Manager", "Technician", "Administrator", "AppAdmin"));

            options.AddPolicy("Manager", policy => policy
            .RequireRole("Manager", "Technician", "Administrator", "AppAdmin"));

            options.AddPolicy("Technician", policy => policy
            .RequireRole("Technician", "Administrator", "AppAdmin"));
            //Technician - every IT employee

            options.AddPolicy("Administrator", policy => policy
            .RequireRole("Administrator", "AppAdmin"));
            //Administrator - Every TeamLeader

            options.AddPolicy("AppAdmin", policy => policy
            .RequireRole("AppAdmin"));
            //Only Developers

            options.FallbackPolicy = options.DefaultPolicy;
        });


        return services;
    }
}