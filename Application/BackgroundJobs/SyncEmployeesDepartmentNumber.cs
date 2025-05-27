using Application.CQRS.ITWarehouseCQRS.Employees.Commands;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Interfaces;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

using Quartz;


//
//  THIS JOB IS FIRED due to shceduler in table.
//
namespace Application.BackgroundJobs;
public class SyncEmployeesDepartmentNumber : IJob
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMediator _mediator;
    public IEmailService _mailService { get; }
    public IConfiguration _configuration { get; }
    private static readonly Dictionary<string, string> vcdToSapMap = new()
    {
        { "01324", "1578" },
        { "19001", "1562" },
        { "19002", "1561" },
        { "19003", "1563" },
        { "19005", "1565" },
        { "19006", "1557" },
        { "19007", "1558" },
        { "19008", "1556" },
        { "19009", "1559" },
        { "19010", "1573" },
        { "19011", "1576" },
        { "19012", "1574" },
        { "19013", "1575" },
        { "19014", "1571" },
        { "19018", "1578" },
        { "19020", "1583" },
        { "19021", "1581" },
        { "19024", "1582" }
    };

    public SyncEmployeesDepartmentNumber(IAppDbContext appDbContext, IMediator mediator, IEmailService mailService, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _mediator = mediator;
        _mailService = mailService;
        _configuration = configuration;
    }
    public async Task Execute(IJobExecutionContext context)
    {

        List<string> errorList = new();
        var empsIQ = await _mediator.Send(new GetAllEmployeesQuery());
        var emps = empsIQ.ToList();
        int counter = 0;
        int isManager = 0;
        int missedVCD = 0;

        foreach(var emp in emps)
        {
            var vcdLocationNumber = emp.VcdCompanyNr;

            try
            {
                vcdToSapMap.TryGetValue(vcdLocationNumber, out var sapNumber);
                emp.SapNumber = sapNumber;
                missedVCD++;

            } catch (Exception ex)
            {
                emp.SapNumber = "Unknown"; // Default value if mapping is not found
                errorList.Add($"Pracownik {emp.LongName} o id: {emp.EnovaEmpId} - ma nieznany VcdCompanyNr.");
                continue;
            }

            if (!emp.IsManager)
            {
                try
                {
                    var man = emps.Where(e => e.EnovaEmpId == emp.ManagerId).First();
                    emp.DeptNumber = man.DeptNumber;
                    counter++;

                }
                catch (Exception ex)
                {
                    errorList.Add($"Błąd podczas przetwarzania id {emp.EnovaEmpId}. Nie odnaleziono przełożonego o id: {emp.ManagerId}. Błąd: {ex.Message}");
                    continue;
                }
                
                
            }
            else
            {

                isManager++;

            }

            await _mediator.Send(new UpdateEmployeeNewCommand(emp));

        }
        errorList.Add($"Managerów: {isManager}");
        errorList.Add($"Zaktualizowano pracowników: {counter}");

        SendEmail("marcin.jarco@porscheinterauto.pl; dawid.urbaniak@porscheinterauto.pl", errorList).Wait();
        await Task.CompletedTask;

    }

    private async Task SendEmail(string rcptEmail, List<string> errorList)
    {
        var _baseUrl = _configuration["BaseUrl"];
        string body = string.Empty;
        string subject = string.Empty;
        string listHTML = string.Empty;
        string subHeaderInfo = string.Empty;
        // Build the table of errors
        int idCounter = 1;
        listHTML = "<table style='border-collapse: collapse; width: 100%;'>"; // Start the table
        listHTML += "<tr><th style='border: 1px solid black; padding: 8px;'>ID</th><th style='border: 1px solid black; padding: 8px;'>Error</th></tr>"; // Table header
        subHeaderInfo = errorList.First();
        errorList.RemoveAt(0);
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

        subject = $"Synchronizacja SapNumber i DeptNumber pracowników wykonana!";
        body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Synchronizacja SapNumber i DeptNumber pracowników wykonana</h1>
                    </div>
                    <div>
                    <h4>{subHeaderInfo}</h4>
                    </p>
                        <p>Zarejestrowano poniższe wydarzenia:</p>

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
