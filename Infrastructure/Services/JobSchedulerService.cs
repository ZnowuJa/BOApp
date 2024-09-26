using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services;
using Quartz;
using Quartz.Spi;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;

public class JobSchedulerService
{
    private readonly IAppDbContext _dbContext;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IServiceProvider _serviceProvider;

    public JobSchedulerService(IAppDbContext dbContext, ISchedulerFactory schedulerFactory, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext;
        _schedulerFactory = schedulerFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task ScheduleJobsAsync()
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var jobs = await _dbContext.BackgroundJobs.Where(job => job.Enabled).ToListAsync();

        foreach (var job in jobs)
        {
            var jobType = Type.GetType(job.JobClass.ToString());
            if (jobType == null)
                continue;

            var jobDetail = JobBuilder.Create(jobType)
                                      .WithIdentity(job.JobClass)
                                      .Build();

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity($"{job.JobClass}-trigger")
                                        .WithCronSchedule(job.CronExpression)
                                        .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }

        await scheduler.Start();
    }
}

