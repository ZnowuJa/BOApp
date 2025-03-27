using System.Text.RegularExpressions;
using Application.CQRS.CoCCQRS.Positions.Queries;
using Application.Interfaces;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

using Quartz;
//Application.BackgroundJobs.ValidateEmployeesForCoC
namespace Application.BackgroundJobs;
public class ValidateEmployeesForCoC : IJob
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMediator _mediator;
    private readonly IEmailService _mailService;
    private readonly IConfiguration _configuration;
    Regex sapRegex = new Regex(@"^15\d{2,4}$");
    Regex emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");


    public ValidateEmployeesForCoC(IAppDbContext appDbContext, IMediator mediator, IEmailService mailService, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _mediator = mediator;
        _mailService = mailService;
        _configuration = configuration;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("JOB STARTED!");
        var emps = await _mediator.Send(new GetAllEmployeesQuery());
        var positions = await _mediator.Send(new GetAllPositionsQuery());
        List<string> positionsNames = positions.Select(x => x.Name.ToLower()).ToList();
        List<string> errors = new List<string>();
        foreach (var employee in emps)
        {
            if (employee.SapNumber == null || !sapRegex.IsMatch(employee.SapNumber))
            { 
                errors.Add($"Problem with SAPNumber for EnovaId: {employee.EnovaEmpId} - either null or not matching pattern");
            }
            if (employee.Email == null || !emailRegex.IsMatch(employee.Email))
            {
                errors.Add($"Problem with Email for EnovaId: {employee.EnovaEmpId} - either null or not matching pattern");
            }
            if (employee.Position == null || !positionsNames.Contains(employee.Position.ToLower()))
            {
                errors.Add($"Problem with Position for EnovaId: {employee.EnovaEmpId}  - either null or not defined in Positions table | src: {employee.Position}");
            }
        }

        string rcptemail = "marcin.jarco@porscheinterauto.pl";
        await SendEmail(rcptemail, errors);
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

       subject = $"Wykryto błędy w imporcie danych pracowników!";
            body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Błędy w imporcie danych pracowników!</h1>
                    </div>
                    <div>
                    </p>
                        
                        <p>Znaleziono poniższe błędy w tabeli Employee:</p>
                        <p> Total errors: {errorList.Count()}</p>
                        {listHTML}
  
                        <p>Pozdrawiamy!</p>
                        <p>Twój zespół Automatyzacji!</p>
                    </div>
                    <div class=""footer"">
                        <p>© 2024 Porsche Inter Auto Polska Sp. z o.o.</p>
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