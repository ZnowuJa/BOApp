using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms.Accounting;
using Application.Interfaces;

using AutoMapper;

using Domain.Forms.Accounting;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

namespace Application.CQRS.AccountingCQRS.BusinessTravels.Commands
{
    public class UpdateBusinessTripCommand(BusinessTravelFormVm item) : IRequest<BusinessTravelFormVm>
    {
        public BusinessTravelFormVm Item { get; set; } = item;
    }

    public class UpdateBusinessTripCommandHandler(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration) : IRequestHandler<UpdateBusinessTripCommand, BusinessTravelFormVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper             = mapper;
        private readonly IEmailService _mailService  = mailService;
        private readonly IConfiguration _configuration = configuration;
        public async Task<BusinessTravelFormVm> Handle(UpdateBusinessTripCommand request, CancellationToken cancellationToken)
        {
            var employee = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.EnovaEmpId)).FirstOrDefaultAsync(cancellationToken);
            var manager = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.LVL1_EnovaEmpId)).FirstOrDefaultAsync(cancellationToken);

            string senderName   = request.Item.EmployeeName;
            string rcptEmail    = manager.Email;
            string rcptName     = manager.LongName;
            string custName     = request.Item.Destination;
            string frmNumber    = request.Item.Number;
            string reason       = request.Item.Objective;
            string rejectReason = request.Item.RejectReason;
            string id           = request.Item.Id.ToString();
            string status       = request.Item.Status;
            string userEmail    = employee.Email;

            using var transaction = await _appDbContext.BeginTransactionAsync();
            try
            {
                var item = await _appDbContext.BusinessTravels.FindAsync(request.Item.Id, cancellationToken);
                if (item != null)
                {
                    _mapper.Map<BusinessTravelForm>(request.Item);
                }
                _appDbContext.BusinessTravels.Update(item);
                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                //_logger.LogInformation($"CreateDeferralPaymentCommandHandler {ex.Message}, {ex.InnerException}");
                throw;
            }

            //await SendEmail(senderName, rcptEmail, rcptName, custName, frmNumber, reason, id, status, userEmail, rejectReason);

            return request.Item;
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

}


