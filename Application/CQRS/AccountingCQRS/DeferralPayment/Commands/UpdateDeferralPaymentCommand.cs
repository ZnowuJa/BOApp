using System.Text.Json;
using Application.Forms;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models;
using System.Data;
using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.Forms.Accounting;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Commands;
public class UpdateDeferralPaymentCommand(DeferralPaymentFormVm item) : IRequest<DeferralPaymentFormVm>
{
    public DeferralPaymentFormVm Item { get; set; } = item;
}
public class UpdateDeferralPaymentCommandHandler(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration, IAsDbContext asDbContext, ILogger<UpdateDeferralPaymentCommandHandler> logger) : IRequestHandler<UpdateDeferralPaymentCommand, DeferralPaymentFormVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IAsDbContext _asDbContext = asDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IEmailService _mailService = mailService;
    private readonly IConfiguration _configuration = configuration;
    private ILogger<UpdateDeferralPaymentCommandHandler> _logger = logger;

    public async Task<DeferralPaymentFormVm> Handle(UpdateDeferralPaymentCommand request, CancellationToken cancellationToken)
    {
        bool orgIsApproved = false;

        var employee = await _appDbContext.Employees.Where(p => p.EnovaEmpId == request.Item.EmployeeId).FirstOrDefaultAsync(cancellationToken);
        var manager = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.LVL1_EnovaEmpId)).FirstOrDefaultAsync(cancellationToken);

        string senderName = request.Item.EmployeeName;
        string rcptEmail = manager.Email;
        string rcptName = manager.LongName;
        string custName = request.Item.KontrahentName;
        string frmNumber = request.Item.Number;
        string reason = request.Item.Note;
        string rejectReason = request.Item.RejectReason;
        string id = request.Item.Id.ToString();
        string status = request.Item.Status;
        string userEmail = employee.Email;

        if (request.Item.Status == "AprobataL2")
        {
            rcptEmail = string.Empty;
            rcptName = "Dział Rozrachunków";

            // Fetch data sequentially rather than using async tasks in parallel
            var emails = new List<string>();
            foreach (var approver in request.Item.Level2Approvers)
            {
                var empl = await _appDbContext.Employees
                    .FirstOrDefaultAsync(p => p.EnovaEmpId == approver.EmpId, cancellationToken);

                if (empl != null && !string.IsNullOrEmpty(empl.Email))
                {
                    emails.Add(empl.Email);
                }
            }

            rcptEmail = string.Join(";", emails);
            //Console.WriteLine();
        }

        using var transaction = await _appDbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            var item = await _appDbContext.DeferralPayments.FindAsync(request.Item.Id, cancellationToken);
            orgIsApproved = item.isApproved;
            if (item != null)
            {
                _mapper.Map(request.Item, item);
                item.Approvals = SerializeApprovals(request.Item.Approvals);
                item.Level1Approvers = SerializeRole(request.Item.Level1Approvers);
                item.Level2Approvers = SerializeRole(request.Item.Level2Approvers);
                item.isApproved = request.Item.isApproved;
            }

            _appDbContext.DeferralPayments.Update(item);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation($"DeferralPayment {item.Id} saved successfully, is Approveed: {item.isApproved} ");
            if (item.isApproved)
            {
                bool spSuccess = false;
                var paymentMethod = item.isApproved ? 1 : 0;
                var customerId = int.Parse(item.KontrahentId);
                _logger.LogInformation($"paymentMethod {paymentMethod} customerID: {customerId} ");
                try
                {

                    // Use EF Core to call the stored procedure
                    var res = await _asDbContext.ChangePaymentMethod(paymentMethod, customerId);
                    _logger.LogInformation($"DeferralPayment res {res} ");
                    spSuccess = res != 0;
                }
                catch (Exception ex)
                {
                    // Log or handle the error appropriately
                    _logger.LogInformation($"DeferralPayment Exception call procedure {ex.Message.ToString()}");

                }

                // After the stored procedure call, update `isApplied`
                if (spSuccess && !item.isApplied)
                {
                    item.isApplied = true;
                    _logger.LogInformation($"DeferralPayment before update with isApplied {item.isApplied}");
                    _appDbContext.DeferralPayments.Update(item);
                    await _appDbContext.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation($"DeferralPayment done");
                }
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        Console.WriteLine();
        await SendEmail(senderName, rcptEmail, rcptName, custName, frmNumber, reason, id, status, userEmail, rejectReason);
        return request.Item;

    }

    private string SerializeApprovals(List<ViewModels.General.ApprovalVm> approvals)
    {
        return approvals == null || approvals.Count == 0 ? null : JsonSerializer.Serialize(approvals);
    }

    private string SerializeRole(List<OrganisationRoleForFormVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }

    private async Task SendEmail(string senderName, string rcptEmail, string rcptName, string custName, string frmNumber, string reason, string id, string status, string userEmail, string rejectreason)
    {
        var _baseUrl = _configuration["BaseUrl"];
        string body = string.Empty;
        string subject = string.Empty;
        var emailAddresses = rcptEmail.Split(';');
        var recipients = emailAddresses.Select(email => new Recipient
        {
            EmailAddress = new EmailAddress
            {
                Address = email.Trim()
            }
        }).ToList();
        //This check here is probably not required.
        if (status == "AprobataL1")
        {
            subject = $"Wniosek o odroczoną płatność ({frmNumber}) oczekuje na aprobatę)";
            body = $@"
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
                        <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/platnoscodroczona/{id}?srcPage=kierownik"">Przejdź do wniosku</a></p>
                        <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/platnosciodroczone/kierownik"">Lista wniosków</a></p>
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
        }
        else if (status == "AprobataL2")
        {
            recipients = new List<Recipient>
            {
                new Recipient
                {
                    EmailAddress = new EmailAddress{ Address = "rozrachunki@porscheinterauto.pl" }
                }

            };
            subject = $"Wniosek o odroczoną płatność ({frmNumber}) oczekuje na aprobatę)";
            body = $@"
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
                        <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/platnoscodroczona/{id}?srcPage=rozrachunki"">Przejdź do wniosku</a></p>
                        <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/platnosciodroczone/rozrachunki"">Lista wniosków</a></p>
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
        }
        else if (status == "Zakończone")
        {
            recipients = new List<Recipient>
            {
                new Recipient
                {
                    EmailAddress = new EmailAddress{ Address = userEmail }
                }

            };
            subject = $"Wniosek o odroczoną płatność ({frmNumber}) został zaaprobowany)";
            body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Wniosek o odroczoną płatność</h1>
                    </div>
                    <div>
                        <p><h3>Twój wniosek o odroczoną płatność numer {frmNumber} został zaaprobowany.</h3></p>
                        <p><h3>Możesz już wystawić dokument z odroczoną płatnością</h3></p>
                        <p>Wniosek dotyczy klienta: <b>{custName}</b></p>
                        <p>Uzasadnienie: <b>{reason}</b></p>
                        <p>Zgłaszający: <b>{senderName}</b></p>
                    </div>
                    <div>
                        <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/platnoscodroczona/{id}?srcPage=pracownik"">Przejdź do wniosku</a></p>
                        <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/platnosciodroczone/pracownik"">Lista wniosków</a></p>
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
        }
        else if (status == "Odrzucone")
        {
            recipients = new List<Recipient>
            {
                new Recipient
                {
                    EmailAddress = new EmailAddress{ Address = userEmail }
                }

            };
            subject = $"Wniosek o odroczoną płatność ({frmNumber}) został odrzucony)";
            body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Wniosek o odroczoną płatność</h1>
                    </div>
                    <div>
                        <p><h3>Twój wniosek o odroczoną płatność numer {frmNumber} został odrzucony.</h3></p>
                        
                        <p>Wniosek dotyczy klienta: <b>{custName}</b></p>
                        <p>Uzasadnienie: <b>{reason}</b></p>
                        <p>Zgłaszający: <b>{senderName}</b></p>
                        <p>Powód odrzucenia: <b>{rejectreason}</b></p>
                    </div>
                    <div>
                        <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/platnosciodroczone/pracownik"">Lista wniosków</a></p>
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
        }

        var message = new Microsoft.Graph.Models.Message
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
