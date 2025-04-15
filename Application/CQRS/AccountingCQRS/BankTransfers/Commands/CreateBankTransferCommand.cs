using Application.CQRS.AccountingCQRS.BusinessTravels.Commands;
using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using Domain.Forms.Accounting;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

namespace Application.CQRS.AccountingCQRS.BankTransfers.Commands;

public class CreateBankTransferCommand(BankTransferFormVm item) : IRequest<int>
    {
        public BankTransferFormVm Item { get; set; } = item;

    }

    public class CreateBankTransferCommandHandler(IAppDbContext context, IMapper mapper, IEmailService mailService, IConfiguration configuration) : IRequestHandler<CreateBankTransferCommand, int>
    {
        private readonly IAppDbContext _appDbContext = context;
        private readonly IMapper _mapper  = mapper;
        private readonly IEmailService _mailService = mailService;
        private readonly IConfiguration _configuration = configuration;
        public async Task<int> Handle(CreateBankTransferCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<BankTransferForm>(request.Item);
            _appDbContext.BankTransfers.Add(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            item.Number = $"{item.NumberPrefix}{item.Id.ToString("D8")}";
            item.StatusId = 1;
            item.Created = DateTime.Now;
            _appDbContext.BankTransfers.Update(item);
            await _appDbContext.SaveChangesAsync();

            #region Send Email
            var employee = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.EnovaEmpId)).FirstOrDefaultAsync();
            var manager = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.LVL1_EnovaEmpId)).FirstOrDefaultAsync();
            string senderName = request.Item.EmployeeName;
            string rcptEmail = manager.Email;
            string rcptName = manager.LongName;
            // string custName = request.Item.Destination;
            string frmNumber = item.Number;
            // string reason = request.Item.Objective;
            string id = item.Id.ToString();
            bool sendMail = _configuration.GetValue<bool>("SendEmail:BusinessTravel");

            // if (!request.Item.SaveOnly && sendMail)
            // {
            //     if (sendMail)
            //     {
            //         await SendEmail(senderName, rcptEmail, rcptName, custName, frmNumber, reason, id);
            //     }
            // }

            #endregion
            return item.Id;
        }
        private async Task SendEmail(string senderName, string rcptEmail, string rcptName, string custName, string frmNumber, string reason, string id)
        {
            var _baseUrl = _configuration["BaseUrl"];
            var subject = $"Nowy wniosek o Delegację ({frmNumber}) :)";
            var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
            </head>
            <body>
                <div class=""header"">
                    <h1>Wniosek o Delegację</h1>
                </div>
                <div>
                    <p><h3>Nowy wniosek o Delegację numer {frmNumber} oczekuje na Twoją aprobatę.</h3></p>
                    <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                    <p>Uzasadnienie: <b>{reason}</b></p>
                    <p>Miasto: <b>{custName}</b></p>
                </div>
                <div>

                    <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/delegacja/{id}?srcPage=kierownik"">Przejdź do wniosku</a></p>
                    <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/delegacje/kierownik"">Lista wniosków</a></p>
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
                            //Address = "marcin.jarco@porscheinterauto.pl"
                        }
                    }
                }
            };

            await _mailService.SendEmailAsync(message);
        }

    }