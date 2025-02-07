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
public class TestEmailJob 
{
    private readonly IEmailService _mailService;
    private readonly IConfiguration _configuration;


    public TestEmailJob(IEmailService mailService, IConfiguration configuration)
    {
        _mailService = mailService;
        _configuration = configuration;
    }

    public async Task Execute()
    {
        Console.WriteLine("JOB STARTED!");
        List<string> errors = new List<string>();
        errors.Add("Test emaila z systemu");
        errors.Add("Test emaila z systemu");




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

       subject = $"Test Email Service";
            body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Test Email Service</h1>
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