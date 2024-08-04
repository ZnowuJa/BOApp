using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using Infrastructure.Identity.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Identity.Client;

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

        //services.AddScoped<GraphServiceClient>(provider =>
        //{
        //    var confidentialClientApplication = ConfidentialClientApplicationBuilder
        //        .Create(configuration["AzureAd:ClientId"])
        //        .WithClientSecret(configuration["AzureAd:ClientSecret"])
        //        .WithAuthority(new Uri($"{configuration["AzureAd:Instance"]}{configuration["AzureAd:TenantId"]}"))
        //        .Build();

        //    var authProvider = new ClientCredentialProvider(confidentialClientApplication);

        //    return new GraphServiceClient(authProvider);
        //});

        //services.AddScoped<ITokenValidatedHandlerService, TokenValidatedHandlerService>();

        //services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        return services;
    }
}
