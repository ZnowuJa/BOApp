using System.Linq;

using Application.CQRS.CoCCQRS.GroupCoCs.Queries;
using Application.CQRS.CoCCQRS.InstructionCoCs.Queries;
using Application.CQRS.CoCCQRS.Positions.Queries;
using Application.CQRS.General.Organisations.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Interfaces;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Quartz;
//Application.BackgroundJobs.MarkEmployeesGroupJob
namespace Application.BackgroundJobs;
public class MarkEmployeesGroupJob 
{

    private readonly IMediator _mediator;

    public MarkEmployeesGroupJob(IMediator mediator)
    {
 
        _mediator = mediator;
    }

    public async Task Execute()
    {
        var today = "2024-05-02";
        //var emps = await _mediator.Send(new GetAllEmployeesByFTEStartDateQuery(today));
        var allemps = await _mediator.Send(new GetAllEmployeesQuery()); //only active employees
        var allEmpsList = allemps.ToList();
        //var instructions = await _mediator.Send(new GetAllInstructionCoCsQuery());
        //var organisations = await _mediator.Send(new GetAllOrganisationsQuery());
        var groups = await _mediator.Send(new GetAllGroupCoCsQuery());
        Console.WriteLine(allEmpsList.Count());
       
        //Managers are in group 1
        allEmpsList.Where(emp => emp.IsManager).ToList().ForEach(emp => emp.CoCGroupId = 1);
        allEmpsList = allemps.Where(emp => !emp.IsManager).ToList();
        Console.WriteLine(allEmpsList.Count());
        
        //Group 3 is for employees with job code 502 and starts with Specjalista
        allEmpsList.Where(emp => (emp.JobCode == "502" && emp.Position.StartsWith("Specjalista"))).ToList().ForEach(emp => emp.CoCGroupId = 3);
        allEmpsList = allEmpsList.Where(emp => !(emp.JobCode == "502" && emp.Position.StartsWith("Specjalista"))).ToList();
        Console.WriteLine(allEmpsList.Count());
        
        //Group 2 is for employees with job code 970 and VCDCOMP 01324
        allEmpsList.Where(emp => (emp.JobCode == "970" && (emp.VcdCompanyNr == "01324" || emp.Position.Contains("Higien")))).ToList().ForEach(emp => emp.CoCGroupId = 2);
        allEmpsList = allEmpsList.Where(emp => !(emp.JobCode == "970" && (emp.VcdCompanyNr == "01324" || emp.Position.Contains("Higien")))).ToList();
        Console.WriteLine(allEmpsList.Count());
        
        //Group 2 is for employees with job code
        var jobCodesExtended = new List<string>
        {
            "103", "301", "303", "304", "305", "307", "308", "309","320", "401", "502", "706", "901", "918", "941", "970", "910_"
        };
        allEmpsList.Where(emp => jobCodesExtended.Contains(emp.JobCode)).ToList().ForEach(emp => emp.CoCGroupId = 2);
        allEmpsList = allEmpsList.Where(emp => !jobCodesExtended.Contains(emp.JobCode)).ToList();
        Console.WriteLine(allEmpsList.Count());

        //Group0 4 
        var jobCodesMinimal = new List<string> { "501", "502", "716", "850", "852", "855", "856", "860", "910", "918" };
        allEmpsList.Where(emp => jobCodesMinimal.Contains(emp.JobCode))
               .ToList()
               .ForEach(emp => emp.CoCGroupId = 2);
        allEmpsList.Where(emp => !jobCodesMinimal.Contains(emp.JobCode))
               .ToList();
        Console.WriteLine(allEmpsList.Count());




        await Task.CompletedTask;
    }
}