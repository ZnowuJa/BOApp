using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Commands
{
    public class UpdateAdvancePaymentCommand(AdvancePaymentFormVm item) : IRequest<AdvancePaymentFormVm>
    {
        public AdvancePaymentFormVm Item { get; set; } = item;
    }

    public class UpdateAdvancePaymentCommandHandler(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration) : IRequestHandler<UpdateAdvancePaymentCommand, AdvancePaymentFormVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _mailService = mailService;
        private readonly IConfiguration _configuration = configuration;
        public async Task<AdvancePaymentFormVm> Handle(UpdateAdvancePaymentCommand request, CancellationToken cancellationToken)
        {
            string environment = _configuration.GetValue<string>("Environment");
            var allEmployees = await _appDbContext.Employees.Where(e => e.IsActive == 1).ToListAsync(cancellationToken);
            var employee = allEmployees.Where(p => p.EnovaEmpId == int.Parse(request.Item.EnovaEmpId)).FirstOrDefault();
            var manager = allEmployees.Where(p => p.EnovaEmpId == int.Parse(request.Item.LVL1_EnovaEmpId)).FirstOrDefault();

            var senderName = request.Item.EmployeeName;

            string rcptEmail = manager.Email;
            string rcptName = manager.LongName;
            string frmNumber = request.Item.Number;
            string reason = request.Item.Objective;
            string rejectReason = request.Item.RejectReason;
            string id = request.Item.Id.ToString();
            string status = request.Item.Status;
            string userEmail = employee.Email;
            string amount = request.Item.AdvancePaymentAmount.ToString();

            using var transaction = await _appDbContext.BeginTransactionAsync();
            try
            {
                var item = await _appDbContext.AdvancePayments.FindAsync(request.Item.Id, cancellationToken);
                if (item != null)
                {
                    _mapper.Map(request.Item, item);
                }
                _appDbContext.AdvancePayments.Update(item);
                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }

            bool sendMail = _configuration.GetValue<bool>("SendEmail:AdvancePayment");
            if (!request.Item.SaveOnly && sendMail)
            {
                //SET RECIPIENTS
                if (status == "AprobataL1")
                {
                    rcptEmail = manager.Email;
                }
                else if (status == "AprobataL2")
                {
                    rcptEmail = allEmployees.Where(e => request.Item.Level5Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).FirstOrDefault();
                    //dla każdego w Level5Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
                }
                else if (status == "ZaliczkaKasa")
                {
                    var emails = allEmployees.Where(e => request.Item.Level2Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
                    rcptEmail = string.Join(";", emails);

                    //dla każdego w Level2Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
                }
                else if (status == "ZaliczkaKsiegowosc")
                {
                    var emails = allEmployees.Where(e => request.Item.Level3Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
                    rcptEmail = string.Join(";", emails);

                    //dla każdego w Level3Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
                }
                else if (status == "ZaliczkaKsiegowoscTL")
                {
                    var emails = allEmployees.Where(e => request.Item.Level4Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
                    rcptEmail = string.Join(";", emails);

                    //dla każdego w Level4Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
                }
                else if (status == "Rozliczone")
                {
                    rcptEmail = employee.Email;
                }
                else if (status == "Odrzucone")
                {
                    rcptEmail = employee.Email;
                }
                await SendEmail(senderName, rcptEmail, rcptName, frmNumber, reason, id, status, userEmail, rejectReason, environment, amount);
            }

            return request.Item;
        }
        private async Task SendEmail(string senderName, string rcptEmail, string rcptName, string frmNumber, string reason, string id, string status, string userEmail, string rejectreason, string env, string amount)
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

            string subjectPrefix = env.ToUpper() == "DEV" ? "##TEST APLIKACJI## " : "";
            string title, listUrl, requestUrl, actionText;
            if (status == "AprobataL1")
            {
                title = "Wniosek o zaliczkę";
                requestUrl = $"{_baseUrl}/zaliczka/{id}?srcPage=kierownik";
                listUrl = $"{_baseUrl}/zaliczki/kierownikL1";
                actionText = "oczekuje na Twoją aprobatę";
            }
            else if (status == "AprobataL2")
            {
                title = "Wniosek o zaliczkę";
                requestUrl = $"{_baseUrl}/zaliczka/{id}?srcPage=kierownik";
                listUrl = $"{_baseUrl}/zaliczki/kierownikL2";
                actionText = "oczekuje na Twoją aprobatę";
            }
            else if (status == "ZaliczkaKasa")
            {
                title = "Wniosek o zaliczkę";
                requestUrl = $"{_baseUrl}/zaliczka/{id}?srcPage=kasjer";
                listUrl = $"{_baseUrl}/zaliczki/kasjer";
                actionText = "oczekuje na wypłatę";
            }
            else if (status == "ZaliczkaKsiegowosc")
            {
                title = "Wniosek o zaliczkę przelewem";
                requestUrl = $"{_baseUrl}/zaliczka/{id}?srcPage=ksiegowe";
                listUrl = $"{_baseUrl}/zaliczki/ksiegowe";
                actionText = "oczekuje na wypłatę";
            }
            else if (status == "ZaliczkaKsiegowoscTL")
            {
                title = "Wniosek o zaliczkę przelewem";
                requestUrl = $"{_baseUrl}/zaliczka/{id}?srcPage=ksiegowe";
                listUrl = $"{_baseUrl}/zaliczki/ksiegowe";
                actionText = "oczekuje na wypłatę";
            }
            else if (status == "Wyplacone")
            {
                title = "Wniosek o zaliczkę";
                requestUrl = $"{_baseUrl}/zaliczka/{id}?srcPage=pracownik";
                listUrl = $"{_baseUrl}/zaliczki";
                actionText = "został rozliczony";
            }
            else if (status == "Odrzucone")
            {
                title = "Wniosek o zaliczkę";
                requestUrl = $"{_baseUrl}/zaliczka/{id}?srcPage=pracownik";
                listUrl = $"{_baseUrl}/zaliczki/pracownik";
                actionText = "został odrzucony";
            }
            else
            {
                return; // Nieobsługiwany status
            }

            subject = $"{subjectPrefix}{title} {frmNumber} {actionText}";
            body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>{title}</h1>
                    </div>
                    <div>
                        <p><h3>{title} numer {frmNumber} {actionText}.</h3></p>
                        <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                        <p>Uzasadnienie: <b>{reason}</b></p>
                        <p>Kwota: <b>{amount} zł</b></p>
                    </div>
                    <div>
                       <p>Kliknij w link, aby przejść do wniosku: <a href=""{requestUrl}"">Przejdź do wniosku</a></p>
                        <p>Przejdź do listy wniosków: <a href=""{listUrl}"">Lista wniosków</a></p>
                    </div>
                    <div>
                        <p>Pozdrawiamy!</p>
                        <p>Twój zespół Automatyzacji!</p>
                    </div>
                    <div class=""footer"">
                        <p>© 2025 Porsche Inter Auto Polska Sp. z o.o.</p> 
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
                ToRecipients = recipients
            };

            await _mailService.SendEmailAsync(message);
        }
    }
}