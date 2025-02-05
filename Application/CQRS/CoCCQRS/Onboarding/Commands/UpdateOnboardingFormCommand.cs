using System.Text.Json;
using Application.Forms.CoC;
using Application.Interfaces;
using Application.ViewModels.CoC;
using Application.ViewModels.General;

using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

namespace Application.CQRS.CoCCQRS.Onboarding.Commands;
public class UpdateOnboardingFormCommand : IRequest<OnboardingFormVm>
{
    public OnboardingFormVm Item { get; set; }

    public UpdateOnboardingFormCommand(OnboardingFormVm item)
    {
        Item = item;
    }
}


public class UpdateOnboardingFormCommandHandler : IRequestHandler<UpdateOnboardingFormCommand, OnboardingFormVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IEmailService _mailService;
    private readonly IConfiguration _configuration;

    public UpdateOnboardingFormCommandHandler(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _mailService = mailService;
        _configuration = configuration;
    }
    public async Task<OnboardingFormVm> Handle(UpdateOnboardingFormCommand request, CancellationToken cancellationToken)
    {


        var _approvals = SerializeApprovals(request.Item.Approvals);
        var _Level1Approvers = SerializeRole(request.Item.Level1Approvers);
        var _Level2Approvers = SerializeRole(request.Item.Level2Approvers);
        var _Instructions = SerializeInstructions(request.Item.Instructions);
        var _group = request.Item.Group;
        var _note = request.Item.Note ?? String.Empty;
        var _progress = request.Item.Progress;
        var _FirstRun = request.Item.FirstRun;
        var _status = request.Item.Status;

        var formItem = await _appDbContext.OnboardingForms.Where(o => o.Id == request.Item.Id).FirstOrDefaultAsync(cancellationToken);

        formItem.Approvals = _approvals;
        formItem.Instructions = _Instructions;
        formItem.Group = _group;
        formItem.Note = _note;
        formItem.Progress = _progress;
        formItem.FirstRun = _FirstRun;
        formItem.Status = _status;

        _appDbContext.OnboardingForms.Update(formItem);
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
