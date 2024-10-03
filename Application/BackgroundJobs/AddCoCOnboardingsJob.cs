using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using Domain.Forms;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Quartz;

namespace Application.BackgroundJobs;
public class AddCoCOnboardingsJob : IJob
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMediator _mediator;
    


    public AddCoCOnboardingsJob(IAppDbContext appDbContext, IMediator mediator)
    {
        _appDbContext = appDbContext;
        _mediator = mediator;
        
    }
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("AddCoCOnboardingsJob has just started...");
        //var today = DateTime.Now.ToString("yyyy-MM-dd");
        var today = "2024-10-01";
        var emps = await _appDbContext.Employees.Where(e => e.FTEStartDate == today).ToListAsync();
        Console.WriteLine($"Today: " + today + " " + emps.Count());
        foreach (var emp in emps)
        {
            var _organisation = await _appDbContext.Organisations.Where(e => e.SapNumber == emp.SapNumber).FirstOrDefaultAsync();
            Console.WriteLine($"Organisation: {_organisation.Name}");
            var onboarding = new OnboardingForm();
        }
        await Task.CompletedTask;
    }

    private string SerializeApprovals(List<Approval> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }

    private string SerializeRole(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }

    private string SerializeInstructions(List<InstructionStatus> items)
    {
        return items == null || items.Count == 0 ? null : JsonSerializer.Serialize(items);
    }
}
