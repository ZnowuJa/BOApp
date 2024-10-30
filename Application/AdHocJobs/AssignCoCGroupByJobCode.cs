using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.ITWarehouseCQRS.Employees.Commands;
using Application.ITWarehouseCQRS.Employees.Queries;

using MediatR;

using Microsoft.Graph.Models;

namespace Application.AdHocJobs;
public class AssignCoCGroupByJobCode
{
    private readonly IMediator _mediator;

    public AssignCoCGroupByJobCode(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Execute()
    {
        //string wcmQuery = "e => e.IsManager == true";
        //string wcQuery = "e => (e.VcdCompanyNr == \"01324\" && e.JobCode == \"970\") || e.JobCode == \"700\" || e.JobCode == \"710\"";
        List<string> minimalGroupJobCodes = new List<string> { "501", "502", "716", "850", "852", "855", "856", "860", "910", "918" };
        //string minimalQuery = "e => minimalGroupJobCodes.Contains(e.JobCode)";
        //White Collar Managers
        var emps = await _mediator.Send(new GetAllEmployeesQuery());

        //string wcmQuery = "IsManager == true";
        //var whiteCollarManager = emps.AsQueryable().Where(wcmQuery).ToList();

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
        //var minimalGroupJobCodes = new List<string> { "501", "502", "716", "850", "852", "855", "856", "860", "910", "918" };
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
