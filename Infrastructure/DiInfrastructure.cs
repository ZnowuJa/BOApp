using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using Infrastructure.Identity.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure;
public static class DiInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostAuthenticationService, PostAuthenticationService>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();
        services.AddScoped<IFileService, FileService>();

        services.AddScoped<IEmailService, GraphEmailService>();
        
        services.AddQuartz();

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        services.AddTransient<IJobSchedulerService, JobSchedulerService>();
        // services.AddSingleton<UserCircuitHandler>();
        // services.AddSingleton<CircuitHandler>(provider => provider.GetRequiredService<UserCircuitHandler>());
        // services.AddScoped<ICircuitService, CircuitService>();
        // services.AddHttpContextAccessor();
        
        // services.AddScoped<ISessionService, SessionService>();
        // services.AddDistributedMemoryCache(); // Required for session state
        // services.AddSession(options =>
        // {
        //     options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
        //     options.Cookie.HttpOnly = true; // Security option
        //     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        //     options.Cookie.SameSite = SameSiteMode.Strict;
        //     options.Cookie.IsEssential = true; // Ensure session works without GDPR consent
        // });
        return services;
    }
}
