using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using Infrastructure.Identity.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DiInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostAuthenticationService, PostAuthenticationService>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();
        //services.AddScoped<ITokenValidatedHandlerService, TokenValidatedHandlerService>();

        //services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        return services;
    }
}
