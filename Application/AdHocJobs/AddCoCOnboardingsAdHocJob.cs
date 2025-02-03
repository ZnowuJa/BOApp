using System.Text.Json;
using Application.CQRS.CoCCQRS.GroupCoCs.Queries;
using Application.CQRS.CoCCQRS.InstructionCoCs.Queries;
using Application.CQRS.CoCCQRS.Onboarding.Commands;
using Application.CQRS.CoCCQRS.Onboarding.Queries;
using Application.CQRS.CoCCQRS.Positions.Queries;
using Application.CQRS.General.Organisations.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Forms.CoC;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Microsoft.Extensions.Configuration;


using Application.ViewModels.General;
using MediatR;



namespace Application.AdHocJobs;
public class AddCoCOnboardingsAdHocJob
{
    //private readonly IAppDbContext _appDbContext;
    private readonly IMediator _mediator;
    private readonly DateTime _from;
    private readonly DateTime _to;
    private readonly IEmailService _mailService;
    private readonly IConfiguration _configuration;

    public AddCoCOnboardingsAdHocJob(IMediator mediator, DateTime from, DateTime to, IEmailService mailService, IConfiguration configuration)
    {
        
        _mediator = mediator;
        _from = from;
        _to = to;
        _mailService = mailService;
        _configuration = configuration;
    }

    public async Task<int> Execute()
    {
        string today;
        var emps = new List<EmployeeVm>();
        var positions = await _mediator.Send(new GetAllPositionsQuery());
        var instructions = await _mediator.Send(new GetAllInstructionCoCsQuery());
        var organisations = await _mediator.Send(new GetAllOrganisationsQuery());
        var groups = await _mediator.Send(new GetAllGroupCoCsQuery());

        if(_from <= _to)
        {
            for (DateTime date = _from; date <= _to; date = date.AddDays(1))
            {
                today = date.ToString("yyyy-MM-dd");
                var tempemps = await _mediator.Send(new GetAllEmployeesByFTEStartDateQuery(today));
                emps.AddRange(tempemps.ToList());

            }
        }

        var onboardings = await _mediator.Send(new GetAllOnboardingsQuery());
        var excludedEmpIds = onboardings.Select(o => o.EmployeeId).ToHashSet();
        emps = emps.Where(emp => !excludedEmpIds.Contains(emp.EnovaEmpId)).ToList();

        foreach (var emp in emps)
        {
            if (!string.IsNullOrEmpty(emp.SapNumber))
            {
                var _organisation = organisations.Where(o => o.SapNumber == emp.SapNumber).FirstOrDefault();
                if (_organisation is null)
                    continue;
                
                //Console.WriteLine($"Organisation: {_organisation.SapNumber}");
                var instStats = new List<InstructionStatus>();
                var groupCoC = groups.Where(gc => gc.Id == emp.CoCGroupId).FirstOrDefault();
                //Console.WriteLine($"Group: {groupCoC.GroupName}");
                if (groupCoC != null)
                {
                    instStats = groupCoC.Instructions.Select(p => new InstructionStatus { InstructionId = p.Id }).ToList();
                }
                var onboarding = new OnboardingFormVm()
                {
                    EmployeeId = emp.EnovaEmpId,
                    EmployeeName = emp.LongName,
                    Approvals = new List<Approval>(),
                    Level1Approvers = _organisation.Role_ComplianceAssistant.Select(role => new OrganisationRoleForFormVm(role)).ToList() ?? new List<OrganisationRoleForFormVm>(),
                    Level2Approvers = _organisation.Role_ComplianceManager.Select(role => new OrganisationRoleForFormVm(role)).ToList() ?? new List<OrganisationRoleForFormVm>(),
                    LVL1_EnovaEmpId = _organisation.Role_ComplianceAssistant.Where(e => e.IsDefault == true).Select(m => m.EmpId).FirstOrDefault().ToString() ?? String.Empty,
                    LVL2_EnovaEmpId = _organisation.Role_ComplianceManager.Where(e => e.IsDefault == true).Select(m => m.EmpId).FirstOrDefault().ToString() ?? String.Empty,
                    LVL1_EmployeeName = _organisation.Role_ComplianceAssistant.Where(e => e.IsDefault).Select(m => m.Employee.LongName).FirstOrDefault() ?? String.Empty,
                    LVL2_EmployeeName = _organisation.Role_ComplianceManager.Where(e => e.IsDefault).Select(m => m.Employee.LongName).FirstOrDefault() ?? String.Empty,
                    ManagerId = emp.ManagerId,
                    Instructions = instStats,
                    Group = groupCoC.GroupName,
                    FirstRun = true
                };
                Console.WriteLine($"onboarding: ready to save");
                try
                {
                    var result = await _mediator.Send(new CreateOnboardingFormCommand(onboarding));
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.Message.ToString());
                }

            }
            else
            {

            }
            
        }
        await Task.CompletedTask;
        return emps.Count;

    }

    public string SerializeApprovals(List<Approval> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }

    private string SerializeRole(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }

    private string SerializeInstructions(List<InstructionStatus> items)
    {
        return items == null || items.Count == 0 ? null : JsonSerializer.Serialize(items);
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
            EmailAddress = new Microsoft.Graph.Models.EmailAddress
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

        var message = new Microsoft.Graph.Models.Message
        {
            Subject = subject,
            Body = new Microsoft.Graph.Models.ItemBody
            {
                ContentType = Microsoft.Graph.Models.BodyType.Html,
                Content = body
            },
            ToRecipients = recipients
        };


        await _mailService.SendEmailAsync(message);
    }
}
