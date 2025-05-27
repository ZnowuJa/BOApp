using System.Reflection;
using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Quartz;

namespace Application;
public static class DiApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {

        var assembly = typeof(DiApplication).Assembly;
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assembly));
        services.AddAutoMapper(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        RegisterJobs(services, assembly);

        return services;
    }

    private static void RegisterJobs(IServiceCollection services, Assembly assembly)
    {
        var jobInterfaceType = typeof(IJob);
        var jobTypes = assembly.GetTypes()
                               .Where(t => jobInterfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (var jobType in jobTypes)
        {
            services.AddTransient(jobInterfaceType, jobType);
        }
    }
}
