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

        // Pobiera aktualnie wykonywane joby
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
            // Pomija aktywne joby
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

            // Jeśli job już istnieje, usuwa go
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

    
    public async Task RunJobManuallyAsync(string jobClass, string assemblyName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var jobClassFullName = $"{jobClass}, {assemblyName}";
        var jobType = Type.GetType(jobClassFullName);

        if (jobType == null)
        {
            _logger.LogWarning($"Job type {jobClass} not found.");
            return;
        }

        var jobKey = new JobKey(jobClass, "DEFAULT");
        var jobDetail = await scheduler.GetJobDetail(jobKey);

        if (jobDetail == null)
        {
            _logger.LogWarning($"Job {jobClass} not found in scheduler.");
            return;
        }

        _logger.LogInformation($"Triggering job {jobClass} manually.");

        await scheduler.TriggerJob(jobKey);
    }
    public async Task<bool> RunJobManuallyAsyncBool(string jobClass, string assemblyName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var jobClassFullName = $"{jobClass}, {assemblyName}";
        var jobType = Type.GetType(jobClassFullName);

        if (jobType == null)
        {
            _logger.LogWarning($"Job type {jobClass} not found.");
            return false;
        }

        var jobKey = new JobKey(jobClass, "DEFAULT");
        var jobDetail = await scheduler.GetJobDetail(jobKey);

        if (jobDetail == null)
        {
            _logger.LogWarning($"Job {jobClass} not found in scheduler.");
            return false;
        }

        _logger.LogInformation($"Triggering job {jobClass} manually.");

        //await scheduler.TriggerJob(jobKey);

        try
        {
            await scheduler.TriggerJob(jobKey);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to trigger job {jobClass}: {ex.Message}");
            return false;
        }
    }
    public async Task RunJobManuallyAsyncWithDate(string jobClass, string assemblyName, DateTime? date)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var jobClassFullName = $"{jobClass}, {assemblyName}";
        var jobType = Type.GetType(jobClassFullName);

        if (jobType == null)
        {
            _logger.LogWarning($"Job type {jobClass} not found.");
            return;
        }

        var jobKey = new JobKey(jobClass, "DEFAULT");
        var jobDetail = await scheduler.GetJobDetail(jobKey);

        if (jobDetail == null)
        {
            _logger.LogWarning($"Job {jobClass} not found in scheduler.");
            return;
        }

        var jobDataMap = new JobDataMap();
        if (date.HasValue)
        {
            jobDataMap.Put("targetDate", date.Value.ToString("yyyy-MM-dd"));
        }

        _logger.LogInformation($"Triggering job {jobClass} manually with date {date?.ToString("yyyy-MM-dd") ?? "not provided"}.");

        await scheduler.TriggerJob(jobKey, jobDataMap);
    }
    public async Task RunJobManuallyAsyncWithDates(string jobClass, string assemblyName, DateTime? from, DateTime? to)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        var jobClassFullName = $"{jobClass}, {assemblyName}";
        var jobType = Type.GetType(jobClassFullName);

        if (jobType == null)
        {
            _logger.LogWarning($"Job type {jobClass} not found.");
            return;
        }

        var jobKey = new JobKey(jobClass, "DEFAULT");
        var jobDetail = await scheduler.GetJobDetail(jobKey);

        if (jobDetail == null)
        {
            _logger.LogWarning($"Job {jobClass} not found in scheduler.");
            return;
        }

        var jobDataMap = new JobDataMap();
        if (from.HasValue && to.HasValue)
        {
            jobDataMap.Put("targetFromDate", from.Value.ToString("yyyy-MM-dd"));
            jobDataMap.Put("targetToDate", to.Value.ToString("yyyy-MM-dd"));
        }

        _logger.LogInformation($"Triggering job {jobClass} manually with date from: {from?.ToString("yyyy-MM-dd") ?? "not provided"} to: {to?.ToString("yyyy-MM-dd") ?? "not provided"}.");

        await scheduler.TriggerJob(jobKey, jobDataMap);
    }
}

