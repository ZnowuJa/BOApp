using System.Text.Json;
using Application.Forms.CoC;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Entities.Common;
using Domain.Forms;

using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

namespace Application.CQRS.CoCCQRS.Onboarding.Commands;
public class CreateOnboardingFormCommand : IRequest<OnboardingFormVm>
{
    public OnboardingFormVm Item { get; set; }

    public CreateOnboardingFormCommand(OnboardingFormVm item)
    {
        Item = item;
    }
}


public class CreateOnboardingFormCommandHandler : IRequestHandler<CreateOnboardingFormCommand, OnboardingFormVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IEmailService _mailService;
    private readonly IConfiguration _configuration;

    public CreateOnboardingFormCommandHandler(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _mailService = mailService;
        _configuration = configuration;
    }
    public async Task<OnboardingFormVm> Handle(CreateOnboardingFormCommand request, CancellationToken cancellationToken)
    {
        var _approvals = SerializeApprovals(request.Item.Approvals);
        var _Level1Approvers = SerializeRole(request.Item.Level1Approvers);
        var _Level2Approvers = SerializeRole(request.Item.Level2Approvers);
        var _Instructions = SerializeInstructions(request.Item.Instructions);
        var _group = request.Item.Group;
        var _note = request.Item.Note ?? String.Empty;
        var _progress = request.Item.Progress;
        var _FirstRun = request.Item.FirstRun;
        var _EmployeeId = request.Item.EmployeeId;
        var _EmployeeName = request.Item.EmployeeName ?? string.Empty;
        var _ManagerId = request.Item.ManagerId;
        var _Requested = DateTime.Now;
        var _LVL1_EnovaEmpId = request.Item.LVL1_EnovaEmpId ?? string.Empty;
        var _LVL2_EnovaEmpId = request.Item.LVL2_EnovaEmpId ?? string.Empty;
        var _LVL1_EmployeeName = request.Item.LVL1_EmployeeName ?? string.Empty;
        var _LVL2_EmployeeName = request.Item.LVL2_EmployeeName ?? string.Empty;
        var _FormFiles = request.Item.FormFiles ?? new List<FormFile>();
        var item = new OnboardingForm()
        {
            Approvals = _approvals,
            Level1Approvers = _Level1Approvers,
            Level2Approvers = _Level2Approvers,
            Instructions = _Instructions,
            Group = _group,
            Note = _note,
            Progress = _progress,
            FirstRun = _FirstRun,
            EmployeeId = _EmployeeId,
            EmployeeName = _EmployeeName,
            ManagerId = _ManagerId,
            Requested = _Requested,
            LVL1_EnovaEmpId = _LVL1_EnovaEmpId,
            LVL2_EnovaEmpId = _LVL2_EnovaEmpId,
            LVL1_EmployeeName = _LVL1_EmployeeName,
            LVL2_EmployeeName = _LVL2_EmployeeName,
            //FormFiles = _FormFiles,
        };

        _appDbContext.OnboardingForms.Add(item);
        await _appDbContext.SaveChangesAsync();

        Console.WriteLine(item.Number);

        item.Number = $"{item.NumberPrefix}{item.Id.ToString("D8")}";

        _appDbContext.OnboardingForms.Update(item);
        await _appDbContext.SaveChangesAsync();


        
        return request.Item;
    }

    private string SerializeApprovals(List<ViewModels.General.ApprovalVm> approvals)
    {
        return approvals == null || approvals.Count == 0 ? string.Empty : JsonSerializer.Serialize(approvals);
    }

    private string SerializeRole(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }

    private string SerializeInstructions(List<InstructionStatus> items)
    {
        return items == null || items.Count == 0 ? string.Empty : JsonSerializer.Serialize(items);
    }

    private async Task SendEmail(string senderName, string rcptEmail, string rcptName, string custName, string frmNumber, string reason, string id)
    {
        var _baseUrl = _configuration["BaseUrl"];
        var subject = $"Onboarding z zakresu wewnętrznych wytycznych (ORG_Anweisung) ({frmNumber}) :)";
        var body = $@"
        <!DOCTYPE html>
        <html>
        <head>
        </head>
        <body>
            <div class=""header"">
                <h1>Wniosek o odroczoną płatność</h1>
            </div>
            <div>
                <p><h3>Nowy wniosek o odroczoną płatność numer {frmNumber} oczekuje na Twoją aprobatę.</h3></p>
                <p>Wniosek dotyczy klienta: <b>{custName}</b></p>
                <p>Uzasadnienie: <b>{reason}</b></p>
                <p>Zgłaszający: <b>{senderName}</b></p>
            </div>
            <div>
                <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/deferralpaymentedit/{id}"">Przejdź do wniosku</a></p>
                <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/deferralpayments"">Lista wniosków</a></p>
            </div>
            <div>
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
            Body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = body
            },
            ToRecipients = new List<Recipient>
        {
            new Recipient
            {
                EmailAddress = new EmailAddress
                {
                    Address = rcptEmail
                }
            }
        }
        };

        await _mailService.SendEmailAsync(message);
    }
}
