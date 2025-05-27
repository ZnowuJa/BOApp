using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Graph.Models;

namespace Infrastructure.BackGroundJobs;
public class QuartzJobService : IHostedService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IServiceProvider _serviceProvider;
    private IScheduler _scheduler;

    public QuartzJobService(ISchedulerFactory schedulerFactory, IServiceProvider serviceProvider)
    {
        _schedulerFactory = schedulerFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        _scheduler.JobFactory = new JobFactory(_serviceProvider);

        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var enabledJobs = context.BackgroundJobs.Where(job => job.Enabled).ToList();

            foreach (var job in enabledJobs)
            {
                IJobDetail jobDetail = JobBuilder.Create<JobFactory>()
                    .WithIdentity(job.Id.ToString())
                    .UsingJobData("ClassName", job.ClassName)
                    .UsingJobData("MethodName", job.MethodName)
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity($"{job.Id}Trigger")
                    .WithCronSchedule(job.CronExpression)
                    .Build();

                await _scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            }
        }

        await _scheduler.Start(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_scheduler != null)
        {
            await _scheduler.Shutdown(cancellationToken);
        }
    }
}

public class JobFactory : IJob
{
    private readonly IServiceProvider _serviceProvider;

    public JobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var jobDetail = context.JobDetail;
        var className = jobDetail.JobDataMap.GetString("ClassName");
        var methodName = jobDetail.JobDataMap.GetString("MethodName");

        var type = Type.GetType(className);
        var method = type.GetMethod(methodName);
        var instance = ActivatorUtilities.CreateInstance(_serviceProvider, type);

        method.Invoke(instance, null);

        return Task.CompletedTask;
    }
}
