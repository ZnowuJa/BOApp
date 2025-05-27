using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using Domain.Forms.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Commands
{
    public class CreateAdvanePaymentCommand(AdvancePaymentFormVm item) : IRequest<int>
    {
        public AdvancePaymentFormVm Item { get; set; } = item;
    }
    public class CreateAdvanePaymentCommandHandler(IAppDbContext context, IMapper mapper, IEmailService mailService, IConfiguration configuration) : IRequestHandler<CreateAdvanePaymentCommand, int>
    {
        private readonly IAppDbContext _appDbContext = context;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _mailService = mailService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<int> Handle(CreateAdvanePaymentCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<AdvancePaymentForm>(request.Item);
            _appDbContext.AdvancePayments.Add(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            item.Number = $"{item.NumberPrefix}{item.Id.ToString("D8")}";
            _appDbContext.AdvancePayments.Update(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            #region Send Email
            var employee = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.EnovaEmpId)).FirstOrDefaultAsync();
            var manager = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.LVL1_EnovaEmpId)).FirstOrDefaultAsync();
            string senderName = request.Item.EmployeeName;
            //string rcptEmail = manager.Email;
            string rcptEmail = "dawid.urbaniak@porscheinterauto.pl";
            string rcptName = manager.LongName;
            //request.Item.Number = item.Number; // Przypisanie numeru wniosku
            string frmNumber = item.Number;
            string reason = request.Item.Objective;
            string amount = request.Item.AdvancePaymentAmount.ToString();

            string id = item.Id.ToString();
            bool sendMail = _configuration.GetValue<bool>("SendEmail:AdvancePayment");

            if (!request.Item.SaveOnly && sendMail)
            {
                if (_configuration.GetValue<bool>("SendEmail:AdvancePayment"))
                {
                    await SendEmail(senderName, rcptEmail, rcptName, frmNumber, reason, id, amount);
                }
            }
            #endregion
            return item.Id;
        }
        private async Task SendEmail(string senderName, string rcptEmail, string rcptName, string frmNumber, string reason, string id, string amount)
        {
            var _baseUrl = _configuration["BaseUrl"];
            var subject = $"Nowy wniosek o Zaliczkę ({frmNumber}) :)";
            var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
            </head>
            <body>
                <div class=""header"">
                    <h1>Wniosek o Zaliczkę</h1>
                </div>
                <div>
                    <p><h3>Nowy wniosek o Zaliczkę numer {frmNumber} oczekuje na Twoją aprobatę.</h3></p>
                    <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                    <p>Uzasadnienie: <b>{reason}</b></p>
                    <p>Kwota: <b>{amount} zł</b></p>
                </div>
                <div>

                    <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/zaliczka/{id}?srcPage=kierownik"">Przejdź do wniosku</a></p>
                    <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/zaliczki/kierownikL1"">Lista wniosków</a></p>
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
                ToRecipients = new List<Recipient>
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = rcptEmail
                            //Address = "marcin.jarco@porscheinterauto.pl"
                        }
                    }
                }
            };

            await _mailService.SendEmailAsync(message);
        }
    }
}
