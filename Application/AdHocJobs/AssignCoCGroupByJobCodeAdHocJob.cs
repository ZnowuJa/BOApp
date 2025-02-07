using Application.CQRS.ITWarehouseCQRS.Employees.Commands;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;

using MediatR;

namespace Application.AdHocJobs;
public class AssignCoCGroupByJobCodeAdHocJob
{
    private readonly IMediator _mediator;

    public AssignCoCGroupByJobCodeAdHocJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Execute()
    {

        List<string> minimalGroupJobCodes = new List<string> { "501", "502", "716", "850", "852", "855", "856", "860", "910", "918" };
        var emps = await _mediator.Send(new GetAllEmployeesQuery());

        var whiteCollarManager = emps.Where(e => e.IsManager == true).ToList();
        foreach (var employee in whiteCollarManager)
        {
            employee.CoCGroupId = 1;
            await _mediator.Send(new UpdateEmployeeCommand(employee));
        }

        //White Collar
        emps = emps.Except(whiteCollarManager);
        var whiteCollars = emps.Where(e => (e.VcdCompanyNr == "01324" && e.JobCode == "970") || e.JobCode == "700" || e.JobCode == "710");
        foreach(var employee in whiteCollars)
        {
            employee.CoCGroupId = 2;
            await _mediator.Send(new UpdateEmployeeCommand(employee));
        }

        //Minimal
        emps = emps.Except(whiteCollars).AsQueryable();
        var minimal = emps.Where(e => minimalGroupJobCodes.Contains(e.JobCode)).ToList();
        foreach (var employee in minimal)
        {
            employee.CoCGroupId = 4;
            await _mediator.Send(new UpdateEmployeeCommand(employee));
        }
        
        emps = emps.Except(minimal).AsQueryable();
        foreach (var employee in emps)
        {
            employee.CoCGroupId = 3;
            await _mediator.Send(new UpdateEmployeeCommand(employee));
        }

        await Task.CompletedTask;
    }

}
