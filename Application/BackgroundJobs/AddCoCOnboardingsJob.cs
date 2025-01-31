using System.Text.Json;
using Application.CQRS.CoCCQRS.GroupCoCs.Queries;
using Application.CQRS.CoCCQRS.InstructionCoCs.Queries;
using Application.CQRS.CoCCQRS.Onboarding.Commands;
using Application.CQRS.CoCCQRS.Positions.Queries;
using Application.CQRS.General.Organisations.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Forms.CoC;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;
using MediatR;
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
        var today = "2024-05-02";
        var emps = await _mediator.Send(new GetAllEmployeesByFTEStartDateQuery(today));
        var allemps = await _mediator.Send(new GetAllEmployeesQuery());
        var positions = await _mediator.Send(new GetAllPositionsQuery());
        var instructions = await _mediator.Send(new GetAllInstructionCoCsQuery());
        var organisations = await _mediator.Send(new GetAllOrganisationsQuery());
        var groups = await _mediator.Send(new GetAllGroupCoCsQuery());

        foreach (var emp in emps)
        {
            var _organisation = organisations.Where(o => o.SapNumber == emp.SapNumber).FirstOrDefault();
            Console.WriteLine($"Organisation: {_organisation.SapNumber}");
            var instStats = new List<InstructionStatus>();
            //var position = positions.Where(p => p.Name == emp.Position).FirstOrDefault();
            //try
            //{
            //    Console.WriteLine($"Position: {position.Name}");
            //}
            //catch 
            //{
            //    continue;
            //}
            
            if (emp != null)
            {
                var groupCoC = groups.Where(gc => gc.Id == emp.CoCGroupId).FirstOrDefault();
                Console.WriteLine($"Group: {groupCoC.GroupName}");
                if (groupCoC != null)
                {
                    //var instsId = 
                    
                    instStats = groupCoC.Instructions.Select(p => new InstructionStatus { InstructionId = p.Id }).ToList();
                    //InstStats is instruction plus initial false
                    Console.WriteLine($"instStats: {instStats.Count()}");
                }

                //Console.WriteLine($"Organisation: {_organisation.Title}");
                var onboarding = new OnboardingFormVm()
                {
                    //WorkflowTemplateId = 2,
                    EmployeeId = emp.EnovaEmpId,
                    EmployeeName = emp.LongName,
                    Approvals = new List<Approval>(),
                    Level1Approvers = _organisation.Role_ComplianceAssistant.Select(role => new OrganisationRoleForFormVm(role)).ToList()??new List<OrganisationRoleForFormVm>(),
                    Level2Approvers = _organisation.Role_ComplianceManager.Select(role => new OrganisationRoleForFormVm(role)).ToList() ?? new List<OrganisationRoleForFormVm>(),
                    LVL1_EnovaEmpId = _organisation.Role_ComplianceAssistant.Where(e => e.IsDefault == true).Select(m => m.EmpId).FirstOrDefault().ToString() ?? String.Empty,
                    LVL2_EnovaEmpId = _organisation.Role_ComplianceManager.Where(e => e.IsDefault == true).Select(m => m.EmpId).FirstOrDefault().ToString() ?? String.Empty,
                    LVL1_EmployeeName = _organisation.Role_ComplianceAssistant.Where(e => e.IsDefault).Select(m => m.Employee.LongName).FirstOrDefault()??String.Empty,
                    LVL2_EmployeeName = _organisation.Role_ComplianceManager.Where(e => e.IsDefault).Select(m => m.Employee.LongName).FirstOrDefault() ?? String.Empty,
                    ManagerId = emp.ManagerId,
                    Instructions = instStats,
                    Group = groupCoC.GroupName,
                    FirstRun = true
                };
                Console.WriteLine($"onboarding: ready to save");
                var result = await _mediator.Send(new CreateOnboardingFormCommand(onboarding));
            }
            else
            {

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
