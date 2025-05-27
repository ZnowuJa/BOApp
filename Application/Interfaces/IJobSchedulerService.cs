namespace Application.Interfaces;
public interface IJobSchedulerService
{
    Task ScheduleJobsAsync();
    Task RunJobManuallyAsync(string jobClass, string assemblyName);

    Task<bool> RunJobManuallyAsyncBool(string jobClass, string assemblyName);
    Task RunJobManuallyAsyncWithDate(string jobClass, string assemblyName, DateTime? date);
    Task RunJobManuallyAsyncWithDates(string jobClass, string assemblyName, DateTime? from, DateTime? to);


}