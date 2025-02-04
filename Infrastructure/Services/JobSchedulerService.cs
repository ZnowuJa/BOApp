using Quartz;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Microsoft.Extensions.Logging;


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

        // Pobierz aktualnie wykonywane joby
        var executingJobs = (await scheduler.GetCurrentlyExecutingJobs())
            .Select(j => j.JobDetail.Key.Name)
            .ToList();

        _logger.LogInformation($"Active jobs: {string.Join(", ", executingJobs)}");

        var jobs = await _dbContext
            .BackgroundJobs
            .Where(j => j.Enabled)
            .AsNoTracking()
            .ToListAsync();
        _logger.LogInformation("Jobs Count: " + jobs.Count);

        foreach (var job in jobs)
        {
            // Pomijaj aktywne joby
            if (executingJobs.Contains(job.JobClass))
            {
                _logger.LogInformation($"Skipping job {job.JobClass} because it is currently being executed.");
                continue;
            }

            var jobClassFullName = $"{job.JobClass}, {job.AssemblyName}";
            var jobType = Type.GetType(jobClassFullName);

            if (jobType == null)
            {
                _logger.LogWarning($"Job type {job.JobClass} not found.");
                continue;
            }

            // Sprawdzenie, czy job o danym identyfikatorze już istnieje w schedulerze
            var existingJob = await scheduler.GetJobDetail(new JobKey(job.JobClass, "DEFAULT"));

            // Jeśli job już istnieje, usuń go
            if (existingJob != null)
            {
                _logger.LogInformation($"Job {job.JobClass} exists. Deleting the old job before creating the new one.");
                await scheduler.DeleteJob(new JobKey(job.JobClass, "DEFAULT"));
            }

            var jobDetail = JobBuilder.Create(jobType)
                                      .WithIdentity(job.JobClass, "DEFAULT")
                                      .Build();

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity($"{job.JobClass}-trigger", "DEFAULT")
                                        .WithCronSchedule(job.CronExpression.ToString())
                                        .StartNow()
                                        .Build();

            _logger.LogInformation($"Scheduling job {job.JobClass} with Cron expression {job.CronExpression}.");

            await scheduler.ScheduleJob(jobDetail, trigger);
        }


        await scheduler.Start();
    }
}

