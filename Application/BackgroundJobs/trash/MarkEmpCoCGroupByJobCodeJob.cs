using Application.CQRS.CoCCQRS.GroupCoCs.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Commands;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Interfaces;
using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

using Quartz;

namespace Application.BackgroundJobs;
public class MarkEmpCoCGroupByJobCodeJob : IJob
{

    private readonly IMediator _mediator;
    public IConfiguration _configuration { get; }
    public IEmailService _mailService { get; }
    public MarkEmpCoCGroupByJobCodeJob(IMediator mediator, IEmailService mailService, IConfiguration configuration)
    {
        _configuration = configuration;
        _mailService = mailService;
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<string> info = new List<string>();

        var allemps = await _mediator.Send(new GetAllEmployeesQuery()); //only active employees
        var allEmpsList = allemps.ToList();
        info.Add($"AllEmps count: {allEmpsList.Count()}");
        var groups = await _mediator.Send(new GetAllGroupCoCsQuery());
        Console.WriteLine(allEmpsList.Count());

        //Managers are in group 1
        var managers = allEmpsList.Where(emp => emp.IsManager).ToList();
        info.Add($"Managers count: {managers.Count()}");
        foreach (var groupmember in managers)
        {
            groupmember.CoCGroupId = 1;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }
        allEmpsList = allemps.Except(managers).ToList();

        Console.WriteLine(allEmpsList.Count());

        //Group 3 is for employees with job code 502 and starts with Specjalista
        var group3And502 = allEmpsList.Where(emp => (emp.JobCode == "502" && (emp.Position.StartsWith("Specjalista")))).ToList();
        info.Add($"Group3 and 502 count: {group3And502.Count()}");
        foreach (var groupmember in group3And502)
        {
            groupmember.CoCGroupId = 3;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));

        }
        allEmpsList = allEmpsList.Except(group3And502).ToList();

        Console.WriteLine(allEmpsList.Count());

        var group41 = allEmpsList.Where(emp => ((emp.JobCode == "970" || emp.JobCode == "918" || emp.JobCode == "502") && (emp.Position.ToLower().Contains("pomoc") || emp.Position.ToLower().Contains("prace") || emp.Position.ToLower().Contains("jako") || emp.Position.ToLower().Contains("kierowca") || emp.Position.ToLower().Contains("wsparcie") || emp.Position.ToLower().Contains("mistrz")))).ToList();
        info.Add($"Group41 count: {group41.Count()}");
        foreach (var groupmember in group41)
        {
            groupmember.CoCGroupId = 4;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }

        allEmpsList = allEmpsList.Except(group41).ToList();

        var group22 = allEmpsList.Where(emp => ((emp.JobCode == "970" || emp.JobCode == "700" || emp.JobCode == "711") && (emp.VcdCompanyNr == "01324" || emp.Position.ToLower().Contains("higien")))).ToList();
        info.Add($"Group22 count: {group22.Count()}");
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
        info.Add($"Group3 count: {group3.Count()}");
        foreach (var groupmember in group3)
        {
            groupmember.CoCGroupId = 3;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }

        allEmpsList = allEmpsList.Except(group3).ToList();

        Console.WriteLine(allEmpsList.Count());

        //Group0 4 
        var jobCodesMinimal = new List<string> { "501", "502", "716", "850", "852", "855", "856", "860", "910", "918" };
        var groupMinimal = allEmpsList.Where(emp => jobCodesMinimal.Contains(emp.JobCode)).ToList();
        info.Add($"Group Minimal count: {groupMinimal.Count()}");
        foreach (var groupmember in groupMinimal)
        {
            groupmember.CoCGroupId = 4;
            await _mediator.Send(new UpdateEmployeeCommand(groupmember));
        }

        allEmpsList = allEmpsList.Except(groupMinimal).ToList();
        info.Add($"Not Matche count: {allEmpsList.Count()}");
        foreach(var a in allEmpsList)
        {
            info.Add($"Not matched: {a.LongName}| EmpId: {a.EnovaEmpId} | Position: {a.Position} | Jobcode: {a.JobCode.ToString()}");
        }
        SendEmail("marcin.jarco@porscheinterauto.pl", info);
        await Task.CompletedTask;
    }
    private async Task SendEmail(string rcptEmail, List<string> errorList)
    {
        var _baseUrl = _configuration["BaseUrl"];
        string body = string.Empty;
        string subject = string.Empty;
        string listHTML = string.Empty;
        // Build the table of errors
        int idCounter = 1;
        listHTML = "<table style='border-collapse: collapse; width: 100%;'>"; // Start the table
        listHTML += "<tr><th style='border: 1px solid black; padding: 8px;'>ID</th><th style='border: 1px solid black; padding: 8px;'>Error</th></tr>"; // Table header

        foreach (var err in errorList)
        {
            listHTML += $"<tr><td style='border: 1px solid black; padding: 8px;'>{idCounter}</td><td style='border: 1px solid black; padding: 8px;'>{err}</td></tr>";
            idCounter++;
        }

        listHTML += "</table>"; // End the table

        var emailAddresses = rcptEmail.Split(';');
        var recipients = emailAddresses.Select(email => new Recipient
        {
            EmailAddress = new EmailAddress
            {
                Address = email.Trim()
            }
        }).ToList();

        subject = $"Wykryto błędy w przypisywaniu Grup COC do pracowników!";
        body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Błędy w przypisywaniu Grup COC do pracowników!</h1>
                    </div>
                    <div>
                    </p>
                        
                        <p>Znaleziono poniższe błędy:</p>
                        <p> Total errors: {errorList.Count()}</p>
                        {listHTML}
  
                        <p>Pozdrawiamy!</p>
                        <p>Twój zespół Automatyzacji!</p>
                    </div>
                    <div class=""footer"">
                        <p>© 2025 Porsche Inter Auto Polska Sp. z o.o.</p>
                    </div>
                </body>
                </html>";

        var message = new Message
        {
            Subject = subject,
            Body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = body
            },
            ToRecipients = recipients
        };


        await _mailService.SendEmailAsync(message);
    }
}

