using Application.CQRS.CoCCQRS.GroupCoCs.Queries;
using Application.CQRS.CoCCQRS.Positions.Queries;
using Application.CQRS.General.Organisations.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Commands;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Interfaces;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

using Quartz;

namespace Application.BackgroundJobs;
public class AssignCoCGroupByPositionJob(IMediator mediator, IEmailService mailService, IConfiguration configuration) : IJob
{
    private readonly IMediator _mediator = mediator;
    private readonly IEmailService _mailService = mailService;
    private readonly IConfiguration _configuration = configuration;

 

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var result = new List<string>();
            var positions = await _mediator.Send(new GetAllPositionsQuery());
            var groups = await _mediator.Send(new GetAllGroupCoCsQuery());
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            var org = await _mediator.Send(new GetOrganisationByEmpSapNumberQuery("157891"));

            foreach (var emp in employees)
            {
                var pos = positions.Where(p => p.Name.ToLower() == emp.Position.ToLower()).FirstOrDefault();
                if (pos != null || pos.GroupCoCId.Value > 0)
                {
                    if (emp.CoCGroupId == pos.GroupCoCId)
                    {
                        continue;
                    }
                    else
                    {
                        var group = groups.Where(g => g.Id == pos.GroupCoCId).FirstOrDefault();
                        if (group != null)
                        {
                            emp.CoCGroupId = pos.GroupCoCId.Value;
                            await _mediator.Send(new UpdateEmployeeCommand(emp));
                            result.Add($"Pracownikowi {emp.LongName} | {emp.EnovaEmpId} przypisano grupę {group.GroupName}");
                        }
                        else
                        {
                            result.Add($"Pracownikowi {emp.LongName} | {emp.EnovaEmpId} nie przypisano grupy dla stanowiska {pos.Name}");


                        }
                    }
                }


            }

            var notAssignedEmployees = employees.Where(e => e.CoCGroupId < 1).ToList();
            if (notAssignedEmployees.Count < 1)
            {
                foreach (var notAssEmp in notAssignedEmployees)
                {
                    result.Add($"Pracownikowi {notAssEmp.LongName} | {notAssEmp.EnovaEmpId}  nie przypisano żadnej grupy");
                }
            }

            var receiversRoles = org.Role_ITManager;
            var emails = string.Join(";", receiversRoles.Select(e => e.Employee.Email));

            await SendEmail(emails, result);
            await Task.CompletedTask;
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            
        }


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
        var recipients = emailAddresses.Select(email => new Microsoft.Graph.Models.Recipient
        {
            EmailAddress = new EmailAddress
            {
                Address = email.Trim()
            }
        }).ToList();

        subject = $"Informacja o aktualizacji GrupCoC na pracowniku";
        body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Informacja o aktualizacji GrupCoC na pracowniku</h1>
                    </div>
                    <div>
                    </p>
                        
                        <p>Znaleziono poniższe zdarzenia:</p>
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
