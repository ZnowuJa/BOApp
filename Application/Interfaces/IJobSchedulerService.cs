namespace Application.Interfaces;
public interface IJobSchedulerService
{
    Task ScheduleJobsAsync();
    Task RunJobManuallyAsync(string jobClass, string assemblyName);
    
}