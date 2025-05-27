using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;

using Microsoft.EntityFrameworkCore;

using Quartz;

namespace Infrastructure.BackGroundJobs;
public class MarkEmployeesGroupJob : IJob
{
    private readonly IAppDbContext _appDbContext;

    public MarkEmployeesGroupJob(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var jobs = await _appDbContext.BackgroundJobs.ToListAsync();
        foreach(var job in jobs)
        {
            Console.WriteLine($"BackgroundJob ID: {job.Id}");
            Console.WriteLine($"JobClass: {job.JobClass}");
            Console.WriteLine($"JobMethod: {job.JobMethod}");
            Console.WriteLine($"CroneExpression: {job.CronExpression}");
            
        }
        await Task.CompletedTask;
    }
}
