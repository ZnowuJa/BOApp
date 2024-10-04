using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Spi;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Application.BackgroundJobs;


namespace Infrastructure.Services;
public class JobSchedulerService : IJobSchedulerService
{
    private readonly IAppDbContext _dbContext;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly ILogger<JobSchedulerService> _logger;

    public JobSchedulerService(IAppDbContext dbContext, ISchedulerFactory schedulerFactory, ILogger<JobSchedulerService> logger)
    {
        _dbContext = dbContext;
        _schedulerFactory = schedulerFactory;
        _logger = logger;
    }

    public async Task ScheduleJobsAsync()
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var jobs = await _dbContext.BackgroundJobs.Where(j => j.Enabled).ToListAsync();
        _logger.LogInformation("Jobs Count: " + jobs.Count);

        foreach (var job in jobs)
        {

            var jobClassFullName = $"{job.JobClass}, {job.AssemblyName}";
            var jobType = Type.GetType(jobClassFullName);

            if (jobType == null)
            {
                _logger.LogWarning($"Job type {job.JobClass} not found.");
                continue;
            }

            var jobDetail = JobBuilder.Create(jobType)
                                      .WithIdentity(job.JobClass, "DEFAULT")
                                      .Build();

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity($"{job.JobClass}-trigger", "DEFAULT")
                                        //.WithCronSchedule(job.CronExpression.ToString())
                                        .StartNow()
                                        .Build();

            _logger.LogInformation($"Scheduling job {job.JobClass} with Cron expression {job.CronExpression}.");

            await scheduler.ScheduleJob(jobDetail, trigger);
        }


        await scheduler.Start();
    }
}

