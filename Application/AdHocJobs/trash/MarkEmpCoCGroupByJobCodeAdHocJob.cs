using Application.CQRS.CoCCQRS.GroupCoCs.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Commands;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;

using MediatR;
//Application.BackgroundJobs.MarkEmployeesGroupJob
namespace Application.BackgroundJobs;
public class MarkEmpCoCGroupByJobCodeAdHocJob 
{

    private readonly IMediator _mediator;

    public MarkEmpCoCGroupByJobCodeAdHocJob(IMediator mediator)
    {
 
        _mediator = mediator;
    }

    public async Task Execute()
    {
        var allemps = await _mediator.Send(new GetAllEmployeesQuery()); //only active employees
        var allEmpsList = allemps.ToList();
        var groups = await _mediator.Send(new GetAllGroupCoCsQuery());
        Console.WriteLine(allEmpsList.Count());

        //Managers are in group 1
        var managers = allEmpsList.Where(emp => emp.IsManager).ToList();
        foreach(var groupmember in managers)
        {
            groupmember.CoCGroupId = 1;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }
        allEmpsList = allemps.Except(managers).ToList();
        
        Console.WriteLine(allEmpsList.Count());

        //Group 3 is for employees with job code 502 and starts with Specjalista
        var group3And502 = allEmpsList.Where(emp => (emp.JobCode == "502" && (emp.Position.StartsWith("Specjalista") ))).ToList();
        foreach(var groupmember in group3And502)
        {
            groupmember.CoCGroupId = 3;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));

        }
        allEmpsList = allEmpsList.Except(group3And502).ToList();

        Console.WriteLine(allEmpsList.Count()); 

        var group41 = allEmpsList.Where(emp => ((emp.JobCode == "970" || emp.JobCode == "918") && (emp.Position.ToLower().Contains("pomoc") || emp.Position.ToLower().Contains("prace") || emp.Position.ToLower().Contains("jako") || emp.Position.ToLower().Contains("kierowca") || emp.Position.ToLower().Contains("wsparcie") || emp.Position.ToLower().Contains("mistrz")))).ToList();

        foreach (var groupmember in group41)
        {
            groupmember.CoCGroupId = 4;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }

        allEmpsList = allEmpsList.Except(group41).ToList();

        var group22 = allEmpsList.Where(emp => ((emp.JobCode == "970" || emp.JobCode == "700"  || emp.JobCode == "711") && (emp.VcdCompanyNr == "01324" || emp.Position.ToLower().Contains("higien")))).ToList();

        foreach (var groupmember in group22)
        {
            groupmember.CoCGroupId = 2;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }

        allEmpsList = allEmpsList.Except(group22).ToList();


        //Group 2 is for employees with job code
        var jobCodesExtended = new List<string>
        {
            "103", "301", "303", "304", "305", "307", "308", "309","320", "401", "502", "706", "901", "918", "941", "970", "910_"
        };
        var group3 = allEmpsList.Where(emp => jobCodesExtended.Contains(emp.JobCode)).ToList();
        foreach(var groupmember in group3)
        {
            groupmember.CoCGroupId = 3;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }
        
        allEmpsList = allEmpsList.Except(group3).ToList();

        Console.WriteLine(allEmpsList.Count());

        //Group0 4 
        var jobCodesMinimal = new List<string> { "501", "502", "716", "850", "852", "855", "856", "860", "910", "918" };
        var groupMinimal = allEmpsList.Where(emp => jobCodesMinimal.Contains(emp.JobCode)).ToList();
        foreach(var groupmember in groupMinimal)
        {
            groupmember.CoCGroupId = 4;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }

        allEmpsList = allEmpsList.Except(groupMinimal).ToList();

        await Task.CompletedTask;
    }
}