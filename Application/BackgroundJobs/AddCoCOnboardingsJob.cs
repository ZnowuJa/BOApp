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
using Application.ViewModels.General;
using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

using Quartz;


//
//  THIS JOB IS FIRED due to shceduler in table.
//
namespace Application.BackgroundJobs;
public class AddCoCOnboardingsJob : IJob
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMediator _mediator;
    public IEmailService _mailService { get; }
    public IConfiguration _configuration { get; }

    public AddCoCOnboardingsJob(IAppDbContext appDbContext, IMediator mediator, IEmailService mailService, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _mediator = mediator;
        _mailService = mailService;
        _configuration = configuration;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        List<string> errorList = new();
        var emps = new List<EmployeeVm>();
        DateTime fromDate;
        DateTime toDate;

        var jobDataMap = context.MergedJobDataMap; 
        if(jobDataMap.Keys.Count() == 2)
        {
            fromDate = DateTime.Parse(jobDataMap.GetString("targetFromDate"));
            toDate = DateTime.Parse(jobDataMap.GetString("targetToDate"));

        } else
        {
            fromDate = DateTime.Now;
            toDate = DateTime.Now;
        }
        errorList.Add($"Zadanie wykonane dla zakresu dat od: {fromDate.ToString("yyyy-MM-dd")} do: {toDate.ToString("yyyy-MM-dd")}");

        if (fromDate <= toDate)
        {
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                var dateToday = date.ToString("yyyy-MM-dd");
                var tempemps = await _mediator.Send(new GetAllEmployeesByFTEStartDateQuery(dateToday));
                emps.AddRange(tempemps.ToList());
                var employeesToRemove = tempemps.Where(e => e.VcdCompanyNr == null || e.SapNumber == null).ToList();
                foreach (var emp in employeesToRemove)
                {
                    emps.Remove(emp);
                    errorList.Add($"Employee {emp.LongName} ({emp.EnovaEmpId}) has no VcdCompanyNr or SAP number");
                }

            }
        }
        else
        {
            errorList.Add("Invalid date range");
        }


        
        
        
        //var positions = await _mediator.Send(new GetAllPositionsQuery());
        var instructions = await _mediator.Send(new GetAllInstructionCoCsQuery());
        var organisations = await _mediator.Send(new GetAllOrganisationsQuery());
        var groups = await _mediator.Send(new GetAllGroupCoCsQuery());

        var onboardings = await _mediator.Send(new GetAllOnboardingsQuery());
        var excludedEmpIds = onboardings.Select(o => o.EmployeeId).ToHashSet();
        var organisationSapNumbers = organisations.Select(o => o.SapNumber).ToHashSet();
        var missingSapNumbers = emps
            .Select(e => e.SapNumber)
            .Where(sapNumber => !organisationSapNumbers.Contains(sapNumber))
            .Distinct();
        errorList.Add(string.Join(", ", missingSapNumbers));
        var excludedEmployees = emps.Where(emp => excludedEmpIds.Contains(emp.EnovaEmpId)).ToList();
        errorList.Add(string.Join(", ", excludedEmployees.Select(e => $"{e.LongName} ({e.EnovaEmpId})")));
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
                    Approvals = new List<ApprovalVm>(),
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
                    errorList.Add(ex.Message.ToString());
                    Console.WriteLine(ex.Message.ToString());
                }

            }
            else
            {
                errorList.Add($"Employee {emp.LongName} ({emp.EnovaEmpId}) has no SAP number");
            }

        }

        SendEmail("marcin.jarco@porscheinterauto.pl; dawid.urbaniak@porscheinterauto.pl", errorList).Wait();
        await Task.CompletedTask;

        
    }


    

    public string SerializeApprovals(List<ApprovalVm> approvals)
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

        subject = $"Wydarzenia podczas generowania nowych formularzy OnBoarding!";
        body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Wydarzenia podczas generowania nowych formularzy OnBoarding</h1>
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
