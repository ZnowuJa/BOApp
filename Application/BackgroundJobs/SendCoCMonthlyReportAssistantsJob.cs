using System.Text.Json;

using Application.CQRS.CoCCQRS.Onboarding.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Interfaces;
using Application.ViewModels.General;

using MediatR;

using Microsoft.Extensions.Configuration;

using Microsoft.Graph.Models;

using Quartz;

namespace Application.BackgroundJobs;
public class SendCoCMonthlyReportAssistantsJob : IJob
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMediator _mediator;
    public IEmailService _mailService { get; }
    public IConfiguration _configuration { get; }

    public SendCoCMonthlyReportAssistantsJob(IAppDbContext appDbContext, IMediator mediator, IEmailService mailService, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _mediator = mediator;
        _mailService = mailService;
        _configuration = configuration;
    }
    public async Task Execute(IJobExecutionContext context)
    {


        List<string> errorList = new();
        var employees = await _mediator.Send(new GetAllEmployeesQuery());
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        var todayDate = DateTime.Now;
        var firstDayOfCurrentMonth = new DateTime(todayDate.Year, todayDate.Month, 1);
        var oneMonthAgo = firstDayOfCurrentMonth.AddMonths(-1);
        var onboardings = await _mediator.Send(new GetAllOnboardingsQuery());

        var onbFinishedLastMonth = onboardings.Where(o => o.Modified >= oneMonthAgo && o.Status == "Zakończone").ToList();
        var onbOpened = onboardings.Where(o => o.Status == "W trakcie").ToList();
        var onbNotStarted = onboardings.Where(o => o.Status == "Rejestracja").ToList();

        var level1Approvers = onboardings.SelectMany(o => o.Level1Approvers).Distinct(new Level1ApproverComparer()).ToList();
        
        foreach (var approver in level1Approvers)
        {
            var approverOnboardings = onboardings.Where(o => o.Level1Approvers.Any(a => a.EmpId == approver.EmpId)).ToList();
            var approverErrorList = new List<string>();

            foreach (var onb in approverOnboardings)
            {
                if (onb.Status == "Rejestracja")
                {
                    var info = $"Nierozpoczęte szkolenie: {onb.EmployeeName}";
                    approverErrorList.Add(info);
                }
                else if (onb.Status == "W trakcie")
                {
                    var info = $"Niezakończone szkolenie: {onb.EmployeeName}";
                    approverErrorList.Add(info);
                }
                else if (onb.Status == "Zakończone" && onb.Modified >= oneMonthAgo)
                {
                    var info = $"Zakończone szkolenie: {onb.EmployeeName}";
                    approverErrorList.Add(info);
                }
            }

            if (approverErrorList.Any())
            {
                bool prod = _configuration["Environment"] == "PROD";

                var appEmpl = employees.Where(e => e.EnovaEmpId == approver.EmpId).FirstOrDefault();
                //SendEmail(appEmpl.Email, approverErrorList, prod).Wait();

                if (appEmpl.EnovaEmpId == 546)
                {
                    SendEmail(appEmpl.Email, approverErrorList, prod).Wait();
                }
                else
                {
                    Console.WriteLine(appEmpl.Email);
                }
            }
        }

        await Task.CompletedTask;

    }

    public string SerializeApprovals(List<ApprovalVm> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }

    private async Task SendEmail(string rcptEmail, List<string> errorList, bool prodEnv)
    {
        var _baseUrl = _configuration["BaseUrl"];
        
        string messageDev = prodEnv ? string.Empty : "<p style='color: red; font-weight: bold;'>Wiadomość pochodzi ze środowiska testowego!</p>";
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

        subject = $"Miesięczny raport postępów - szkolenia Onboarding";
        body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Miesięczny raport postępów - szkolenia Onboarding</h1>
                    </div>
                    <div>
                    </p>
                        {messageDev}
                        
                        <p></p>
                        
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
public class Level1ApproverComparer : IEqualityComparer<OrganisationRoleForFormVm>
{
    public bool Equals(OrganisationRoleForFormVm x, OrganisationRoleForFormVm y)
    {
        if (x == null || y == null)
            return false;

        return x.EmpId == y.EmpId; // Compare based on EmpId or other unique property
    }

    public int GetHashCode(OrganisationRoleForFormVm obj)
    {
        return obj.EmpId.GetHashCode(); // Use the same property for hash code
    }
}