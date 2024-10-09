using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Application.CQRS.CoCCQRS.GroupCoCs.Queries;
using Application.CQRS.CoCCQRS.InstructionCoCs.Queries;
using Application.CQRS.CoCCQRS.Onboarding.Commands;
using Application.CQRS.CoCCQRS.Positions.Queries;
using Application.CQRS.General.Organisations.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Forms;
using Application.Interfaces;
using Application.ITWarehouseCQRS.Employees.Queries;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using Domain.Entities.CoC;
using Domain.Forms;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Quartz;
//Application.BackgroundJobs.AddCoCOnboardingsJob
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
        //Console.WriteLine("AddCoCOnboardingsJob has just started...");
        //var today = DateTime.Now.ToString("yyyy-MM-dd");
        var today = "2024-09-01";
        var emps = await _mediator.Send(new GetAllEmployeesByFTEStartDateQuery(today));
        var allemps = await _mediator.Send(new GetAllEmployeesQuery());
        var positions = await _mediator.Send(new GetAllPositionsQuery());
        var instructions = await _mediator.Send(new GetAllInstructionCoCsQuery());
        var organisations = await _mediator.Send(new GetAllOrganisationsQuery());
        var groups = await _mediator.Send(new GetAllGroupCoCsQuery());

        //var emps = await _appDbContext.Employees.Where(e => e.FTEStartDate == today).ToListAsync();
        //var allemps = await _appDbContext.Employees.Where(e => e.IsActive == 1).ToListAsync();
        //var positions = await _appDbContext.Positions.Where(p => p.StatusId == 1).ToListAsync();
        //var instructions = await _appDbContext.Instructions.Where(i => i.StatusId == 1).ToListAsync();
        //var organisations = await _appDbContext.Organisations.Where(o => o.StatusId == 1).ToListAsync();
        //var groups = await _appDbContext.Groups.Include(p => p.Positions).Include(i => i.Instructions).ToListAsync();

        foreach (var emp in emps)
        {
            var _organisation = organisations.Where(o => o.SapNumber == emp.SapNumber).FirstOrDefault();
            Console.WriteLine($"Organisation: {_organisation.SapNumber}");
            var instStats = new List<InstructionStatus>();
            var position = positions.Where(p => p.Name == emp.Position).FirstOrDefault();
            Console.WriteLine($"Position: {position.Name}");
            if (position != null)
            {
                var groupCoC = groups.Where(gc => gc.Id == position.GroupCoCId).FirstOrDefault();
                Console.WriteLine($"Group: {groupCoC.GroupName}");
                if (groupCoC != null)
                {
                    //var instsId = 
                    
                    instStats = groupCoC.Instructions.Select(p => new InstructionStatus { InstructionId = p.Id }).ToList();
                    //InstStats is instruction plus initial false
                    Console.WriteLine($"instStats: {instStats.Count()}");
                }

                //Console.WriteLine($"Organisation: {_organisation.Name}");
                var onboarding = new OnboardingFormVm()
                {
                    //WorkflowTemplateId = 2,
                    EmployeeId = emp.EnovaEmpId,
                    EmployeeName = emp.LongName,
                    Approvals = new List<Approval>(),
                    Level1Approvers = _organisation.Role_ComplianceAssistant.Select(role => new OrganisationRoleForFormVm(role)).ToList(),
                    Level2Approvers = _organisation.Role_ComplianceManager.Select(role => new OrganisationRoleForFormVm(role)).ToList(),
                    LVL1_EnovaEmpId = _organisation.Role_ComplianceAssistant.Where(e => e.IsDefault == true).Select(m => m.EmpId).FirstOrDefault().ToString(),
                    LVL2_EnovaEmpId = _organisation.Role_ComplianceManager.Where(e => e.IsDefault == true).Select(m => m.EmpId).FirstOrDefault().ToString(),
                    LVL1_EmployeeName = _organisation.Role_ComplianceAssistant.Where(e => e.IsDefault).Select(m => m.Employee.LongName).FirstOrDefault(),
                    LVL2_EmployeeName = _organisation.Role_ComplianceManager.Where(e => e.IsDefault).Select(m => m.Employee.LongName).FirstOrDefault(),
                    ManagerId = emp.ManagerId,
                    Instructions = instStats,
                    Group = groupCoC.GroupName,
                    FirstRun = true
                };
                Console.WriteLine($"onboarding: ready to save");
                var result = _mediator.Send(new CreateOnboardingFormCommand(onboarding));


            }
            await Task.CompletedTask;
        }


    }

    public string SerializeApprovals(List<Approval> approvals)
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
