using System.Text.Json;
using Application.Forms;
using AutoMapper;
using Domain.Forms;
using Application.Interfaces;
using Application.ViewModels.General;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;
using Microsoft.EntityFrameworkCore;
using Application.Forms.Accounting;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Commands
{
    public class CreateDeferralPaymentSQLCommand(DeferralPaymentFormVm item) : IRequest<DeferralPaymentFormVm>
    {
        public DeferralPaymentFormVm Item { get; set; } = item;
    }

    public class CreateDeferralPaymentSQLCommandHandler(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration) : IRequestHandler<CreateDeferralPaymentSQLCommand, DeferralPaymentFormVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _mailService = mailService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<DeferralPaymentFormVm> Handle(CreateDeferralPaymentSQLCommand request, CancellationToken cancellationToken)
        {

            using var transaction = await _appDbContext.BeginTransactionAsync();
            try
            {
                var item = _mapper.Map<DeferralPaymentForm>(request.Item);
                item.Approvals = SerializeApprovals(request.Item.Approvals);
                item.Level1Approvers = SerializeRole(request.Item.Level1Approvers);
                item.Level2Approvers = SerializeRole(request.Item.Level2Approvers);

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
                //_logger.LogInformation($"CreateDeferralPaymentCommandHandler {ex.Message}, {ex.InnerException}");
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


            await SendEmail(senderName, rcptEmail, rcptName, custName, frmNumber, reason, id);
            //_logger.LogInformation($"CreateDeferralPaymentCommandHandler {request.Item.EmployeeName}");
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
}