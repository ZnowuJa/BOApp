using System.Text.Json;

using Application.AAA_IDEAS;
using Application.Forms;
using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms;

using MediatR;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Commands;
public class CreateDeferralPaymentCommandHandler : IRequestHandler<CreateDeferralPaymentCommand, DeferralPaymentFormVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IEmailService _mailService;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public CreateDeferralPaymentCommandHandler(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration, ILogger logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _mailService = mailService;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<DeferralPaymentFormVm> Handle(CreateDeferralPaymentCommand request, CancellationToken cancellationToken)
    {

        using var transaction = await _appDbContext.BeginTransactionAsync();
        try
        {
            var item = _mapper.Map<DeferralPaymentForm>(request.Item);
            item.Approvals = SerializeApprovals(request.Item.Approvals);
            item.Level1Approvers = SerializeRole(request.Item.Level1Approvers);
            item.Level2Approvers = SerializeRole(request.Item.Level2Approvers);
            //// Check if the WorkflowTemplate is already being tracked
            //var trackedWorkflowTemplate = await _appDbContext.WorkflowTemplates
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(wt => wt.Id == item.WorkflowTemplateId);

            //if (trackedWorkflowTemplate != null)
            //{
            //    // Attach the existing instance to avoid tracking issues
            //    _appDbContext.WorkflowTemplates.Attach(trackedWorkflowTemplate);
            //    item.WorkflowTemplate = trackedWorkflowTemplate;
            //}
            _appDbContext.DeferralPayments.Add(item);
            await _appDbContext.SaveChangesAsync();

            item.Number = $"{item.NumberPrefix}{item.Id.ToString("D8")}";
            item.StatusId = 1;
            _appDbContext.DeferralPayments.Update(item);
            item.Requested = DateTime.Now;
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            request.Item.Id = item.Id;
            request.Item.Number = item.Number;
            request.Item.Status = item.Status;
            request.Item.Requested = item.Requested;

        }
        catch (Exception ex)
        {

            await transaction.RollbackAsync();
            _logger.LogInformation($"CreateDeferralPaymentCommandHandler {ex.Message}, {ex.InnerException}");
            throw;
        }
        var employee = await _appDbContext.Employees.Where(p => p.EnovaEmpId == request.Item.EmployeeId).FirstOrDefaultAsync();
        var manager = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.LVL1_EnovaEmpId)).FirstOrDefaultAsync();

        string senderName = request.Item.EmployeeName;
        string rcptEmail = manager.Email;
        string rcptName = manager.LongName;
        string custName = request.Item.KontrahentName;
        string frmNumber = request.Item.Number; 
        string reason = request.Item.Note; 
        string id = request.Item.Id.ToString();


        await SendEmail(senderName, rcptEmail, rcptName, custName, frmNumber, reason, id );
        _logger.LogInformation($"CreateDeferralPaymentCommandHandler {request.Item.EmployeeName}");
        return request.Item;
    }
    private string SerializeApprovals(List<ViewModels.General.Approval> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }

    private string SerializeRole(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }

    private async Task SendEmail(string senderName, string rcptEmail, string rcptName, string custName, string frmNumber, string reason, string id)
    {
        var _baseUrl = _configuration["BaseUrl"];
        var subject = $"Nowy wniosek o odroczoną płatność ({frmNumber}) :)";
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
                <p>© 2024 PIAPL BackOfficeApp Team</p>
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
