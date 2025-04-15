using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms.Accounting;
using Application.Forms.Accounting.BuisnessTravelSmallClasses;
using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms.Accounting;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

namespace Application.CQRS.AccountingCQRS.BankTransfers.Commands
{
    public class UpdateBankTransferFormCommand(BankTransferFormVm item) : IRequest<BankTransferFormVm>
    {
        public BankTransferFormVm Item { get; set; } = item;
    }

    public class UpdateBankTransferFormCommandHandler(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration) : IRequestHandler<UpdateBankTransferFormCommand, BankTransferFormVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper             = mapper;
        private readonly IEmailService _mailService  = mailService;
        private readonly IConfiguration _configuration = configuration;
        public async Task<BankTransferFormVm> Handle(UpdateBankTransferFormCommand request, CancellationToken cancellationToken)
        {
            string environment = _configuration.GetValue<string>("Environment");
            var allEmployees = await _appDbContext.Employees.Where(e => e.IsActive == 1).ToListAsync(cancellationToken);
            var employee = allEmployees.Where(p => p.EnovaEmpId == int.Parse(request.Item.EnovaEmpId)).FirstOrDefault();
            var manager = allEmployees.Where(p => p.EnovaEmpId == int.Parse(request.Item.LVL1_EnovaEmpId)).FirstOrDefault();

            string senderName   = request.Item.EmployeeName;

            string rcptEmail    = manager.Email;
            string rcptName     = manager.LongName;
            // string custName     = request.Item.Destination;
            string frmNumber    = request.Item.Number;
            // string reason       = request.Item.Objective;
            string rejectReason = request.Item.RejectReason;
            string id           = request.Item.Id.ToString();
            string status       = request.Item.Status;
            string userEmail    = employee.Email;
            string emailList = string.Empty;
            using var transaction = await _appDbContext.BeginTransactionAsync();
            try
            {
                var item = await _appDbContext.BankTransfers.FindAsync(request.Item.Id, cancellationToken);
                if (item != null)
                {
                    _mapper.Map(request.Item, item);
                }
                _appDbContext.BankTransfers.Update(item);
                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                //_logger.LogInformation($"CreateDeferralPaymentCommandHandler {ex.Message}, {ex.InnerException}");
                throw;
            }

            bool sendMail = _configuration.GetValue<bool>("SendEmail:BusinessTravel");

            // if (!request.Item.SaveOnly && sendMail)
            // {
            //     //SET RECIPIENTS
            //     if (status == "AprobataL1")
            //     {
            //         rcptEmail = manager.Email;
            //     }
            //     else if (status == "ZaliczkaKasa")
            //     {
            //         var emails = allEmployees.Where(e => request.Item.Level2Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
            //         rcptEmail = string.Join(";", emails);

            //         //dla każdego w Level2Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
            //     }
            //     else if (status == "ZaliczkaKsiegowosc")
            //     {
            //         var emails = allEmployees.Where(e => request.Item.Level3Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
            //         rcptEmail = string.Join(";", emails);

            //         //dla każdego w Level3Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
            //     }
            //     else if (status == "ZaliczkaKsiegowoscTL")
            //     {
            //         var emails = allEmployees.Where(e => request.Item.Level4Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
            //         rcptEmail = string.Join(";", emails);

            //         //dla każdego w Level4Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
            //     }
            //     else if (status == "Rozliczenie")
            //     {
            //         rcptEmail = employee.Email;
            //     }
            //     else if (status == "Ksiegowosc")
            //     {
            //         var emails = allEmployees.Where(e => request.Item.Level2Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
            //         rcptEmail = string.Join(";", emails);
            //         //dla każdego w Level3Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
            //     }
            //     else if (status == "KsiegowoscTL")
            //     {
            //         var emails = allEmployees.Where(e => request.Item.Level4Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
            //         rcptEmail = string.Join(";", emails);
            //         //dla każdego w Level4Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
            //     }
            //     else if (status == "AprobataL11" )
            //     {
            //         if(request.Item.FormCostCenters.Count() > 1)
            //         {
            //             var emails = allEmployees.Where(e => request.Item.FormCostCenters.Any(or => or.ResponsibleManagerEnovaId == e.EnovaEmpId)).Select(e => e.Email).ToList();
            //             emails.Remove(manager.Email);
            //             emails.Insert(0, manager.Email);
            //             rcptEmail = string.Join(";", emails);

            //         } else
            //         {
            //             rcptEmail = manager.Email;
            //         }
                        
            //     }
            //     else if (status == "AprobataL12")
            //     {
            //         rcptEmail = allEmployees.Where(e => request.Item.Level5Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).FirstOrDefault();
            //         //dla każdego w Level5Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
            //     }
            //     else if (status == "KasaRozliczenie")
            //     {
            //         var emails = allEmployees.Where(e => request.Item.Level2Approvers.Any(or => or.EmpId == e.EnovaEmpId)).Select(e => e.Email).ToList();
            //         rcptEmail = string.Join(";", emails);
            //         //dla każdego w Level2Approvers pobrać empId, potem adres email i dodać do listy ze średnikiem.
            //     }
            //     else if (status == "Rozliczone")
            //     {
            //         rcptEmail = employee.Email;
            //     }
            //     else if (status == "Odrzucone")
            //     {
            //         rcptEmail = employee.Email;
            //     }

                
            //         await SendEmail(senderName, rcptEmail, rcptName, custName, frmNumber, reason, id, status, userEmail, rejectReason, environment);
               
                    
            // }

            

            return request.Item;
        }

        private async Task SendEmail(string senderName, string rcptEmail, string rcptName, string custName, string frmNumber, string reason, string id, string status, string userEmail, string rejectreason, string env)
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
                if(env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Nowy wniosek o Delegację {frmNumber}";
                } else
                {
                    subject = $"Nowy wniosek o Delegację {frmNumber}";
                }
                //subject = $"Nowy wniosek o Delegację {frmNumber}";
                body = $@"
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
            }
            else if (status == "ZaliczkaKasa" )
            {
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Wniosek o zaliczkę na Delegację {frmNumber}";
                }
                else
                {
                    subject = $"Wniosek o zaliczkę na Delegację {frmNumber}";
                }
                //subject = $"Wniosek o zaliczkę na Delegację {frmNumber}";
                body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Wniosek o zaliczkę na Delegację</h1>
                    </div>
                    <div>
                        <p><h3>Nowy wniosek o zaliczkę na Delegację numer {frmNumber} oczekuje na wypłatę.</h3></p>
                        <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                        <p>Uzasadnienie: <b>{reason}</b></p>
                        <p>Miasto: <b>{custName}</b></p>
                        
                    </div>
                    <div>
                       <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/delegacja/{id}?srcPage=kasjer"">Przejdź do wniosku</a></p>
                        <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/delegacje/kasjer"">Lista wniosków</a></p>
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
            else if (status == "ZaliczkaKsiegowosc")
            {
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Wniosek o zaliczkę przelewem na Delegację {frmNumber}";
                }
                else
                {
                    subject = $"Wniosek o zaliczkę przelewem na Delegację {frmNumber}";
                }
                //subject = $"Wniosek o zaliczkę przelewem na Delegację {frmNumber}";
                body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Wniosek o zaliczkę przelewem na Delegację</h1>
                    </div>
                    <div>
                        <p><h3>Nowy wniosek o zaliczkę na Delegację numer {frmNumber} oczekuje na wypłatę.</h3></p>
                        <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                        <p>Uzasadnienie: <b>{reason}</b></p>
                        <p>Miasto: <b>{custName}</b></p>
                        
                    </div>
                    <div>
                       <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/delegacja/{id}?srcPage=ksiegowe"">Przejdź do wniosku</a></p>
                        <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/delegacje/ksiegowe"">Lista wniosków</a></p>
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
            else if (status == "ZaliczkaKsiegowoscTL")
            {
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Wniosek o zaliczkę przelewem na Delegację {frmNumber}";
                }
                else
                {
                    subject = $"Wniosek o zaliczkę przelewem na Delegację {frmNumber}";
                }
                //subject = $"Wniosek o zaliczkę przelewem na Delegację {frmNumber}";
                body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Wniosek o zaliczkę przelewem na Delegację</h1>
                    </div>
                    <div>
                        <p><h3>Nowy wniosek o zaliczkę na Delegację numer {frmNumber} oczekuje na wypłatę.</h3></p>
                        <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                        <p>Uzasadnienie: <b>{reason}</b></p>
                        <p>Miasto: <b>{custName}</b></p>
                        
                    </div>
                    <div>
                       <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}/delegacja/{id}?srcPage=ksiegowe"">Przejdź do wniosku</a></p>
                        <p>Przejdź do listy wniosków: <a href=""{_baseUrl}/delegacje/ksiegowe"">Lista wniosków</a></p>
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
            else if (status == "Rozliczenie")
            {
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Wniosek o Delegację {frmNumber}";
                }
                else
                {
                    subject = $"Wniosek o Delegację {frmNumber}";
                }
                //subject = $"Wniosek o Delegację {frmNumber}";
                body = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                        <div class=""header"">
                            <h1>Wniosek o Delegację</h1>
                        </div>
                        <div>
                            <p><h3>Wniosek o Delegację numer {frmNumber} oczekuje na rozliczenie.</h3></p>
                            <p>Uzasadnienie: <b>{reason}</b></p>
                            <p>Miasto: <b>{custName}</b></p>
                        </div>
                        <div>
                            <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}//delegacja/{id}?srcPage=pracownik"">Przejdź do wniosku</a></p>
                            <p>Przejdź do aplikacji <a href=""{_baseUrl}/delegacje"">Lista wniosków</a></p>
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
            else if (status == "Ksiegowosc")
            {
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Wniosek o Delegację {frmNumber} - rozliczenie";
                }
                else
                {
                    subject = $"Wniosek o Delegację {frmNumber} - rozliczenie";
                }
                //subject = $"Wniosek o Delegację {frmNumber} - rozliczenie";
                body = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                        <div class=""header"">
                            <h1>Wniosek o Delegację - rozliczenie</h1>
                        </div>
                        <div>
                            <p><h3>Rozliczenie wniosku o Delegację numer {frmNumber} oczekuje na aprobatę.</h3></p>
                            <p>Uzasadnienie: <b>{reason}</b></p>
                            <p>Miasto: <b>{custName}</b></p>
                        </div>
                        <div>
                            <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}//delegacja/{id}?srcPage=ksiegowe"">Przejdź do wniosku</a></p>
                            <p>Przejdź do aplikacji <a href=""{_baseUrl}/delegacje"">Lista wniosków</a></p>
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
            else if (status == "KsiegowoscTL")
            {
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Wniosek o Delegację {frmNumber} - rozliczenie";
                }
                else
                {
                    subject = $"Wniosek o Delegację {frmNumber} - rozliczenie";
                }
                //subject = $"Wniosek o Delegację {frmNumber} - rozliczenie";
                body = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                        <div class=""header"">
                            <h1>Wniosek o Delegację - rozliczenie</h1>
                        </div>
                        <div>
                            <p><h3>Rozliczenie wniosku o Delegację numer {frmNumber} oczekuje na aprobatę.</h3></p>
                            <p>Uzasadnienie: <b>{reason}</b></p>
                            <p>Miasto: <b>{custName}</b></p>
                        </div>
                        <div>
                            <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}//delegacja/{id}?srcPage=ksiegowe"">Przejdź do wniosku</a></p>
                            <p>Przejdź do aplikacji <a href=""{_baseUrl}/delegacje"">Lista wniosków</a></p>
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
            else if (status == "AprobataL11" || status == "AprobataL12")
            {

                if (recipients.Count > 1)
                {
                    if (env.ToUpper() == "DEV")
                    {
                        subject = $"##TEST APLIKACJI## Informacja o wniosku o Delegację {frmNumber}";
                    }
                    else
                    {
                        subject = $"Informacja o wniosku o Delegację {frmNumber}";
                    }
                    //subject = $"Informacja o wniosku o Delegację {frmNumber}";
                    body = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                        <div class=""header"">
                            <h1>Informacja o wniosku o Delegację</h1>
                        </div>
                        <div>
                            <p><h3>Informację o rozliczenie wniosku o Delegację numer {frmNumber}.</h3></p>
                            <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                            <p>Uzasadnienie: <b>{reason}</b></p>
                            <p>Miasto: <b>{custName}</b></p>
                        </div>
                            <div>
                                <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}//delegacja/{id}?srcPage=view"">Przejdź do wniosku</a></p>
                                <p>Przejdź do aplikacji <a href=""{_baseUrl}/delegacje"">Lista wniosków</a></p>
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

                    var messageMulti = new Microsoft.Graph.Models.Message
                    {
                        Subject = subject,
                        Body = new ItemBody
                        {
                            ContentType = BodyType.Html,
                            Content = body
                        },
                        ToRecipients = recipients.Skip(1).ToList()
                    };

                    await _mailService.SendEmailAsync(messageMulti);
                }
                else
                {
                    if (env.ToUpper() == "DEV")
                    {
                        subject = $"##TEST APLIKACJI## Rozliczenie wniosku o Delegację {frmNumber} oczekuje na aprobatę";
                    }
                    else
                    {
                        subject = $"Rozliczenie wniosku o Delegację {frmNumber} oczekuje na aprobatę";
                    }
                    //subject = $"Rozliczenie wniosku o Delegację {frmNumber} oczekuje na aprobatę";
                    body = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                        <div class=""header"">
                            <h1>Rozliczenie wniosku o Delegację</h1>
                        </div>
                        <div>
                            <p><h3>Rozliczenie wniosku o Delegację numer {frmNumber} oczekuje na Twoją aprobatę.</h3></p>
                            <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                            <p>Uzasadnienie: <b>{reason}</b></p>
                            <p>Miasto: <b>{custName}</b></p>
                        </div>
                            <div>
                                <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}//delegacja/{id}?srcPage=kierownik"">Przejdź do wniosku</a></p>
                                <p>Przejdź do aplikacji <a href=""{_baseUrl}/delegacje"">Lista wniosków</a></p>
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
                    recipients = recipients.Take(1).ToList();
                }
                   
            }
            else if (status == "KasaRozliczenie")
            {
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Rozliczenie wniosku o Delegację {frmNumber} oczekuje na wypłatę";
                }
                else
                {
                    subject = $"Rozliczenie wniosku o Delegację {frmNumber} oczekuje na wypłatę";
                }
                //subject = $"Rozliczenie wniosku o Delegację {frmNumber} oczekuje na wypłatę)";
                body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Rozliczenie wniosku o Delegację</h1>
                    </div>
                    <div>
                        <p><h3>Rozliczenie wniosku o Delegację numer {frmNumber} oczekuje na wypłatę.</h3></p>
                        <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                        <p>Uzasadnienie: <b>{reason}</b></p>
                        <p>Miasto: <b>{custName}</b></p>
                    </div>
                        <div>
                            <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}//delegacja/{id}?srcPage=kasjer"">Przejdź do wniosku</a></p>
                            <p>Przejdź do aplikacji <a href=""{_baseUrl}/delegacje"">Lista wniosków</a></p>
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
            else if (status == "Rozliczone")
            {
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Wniosek o Delegację {frmNumber} został rozliczony";
                }
                else
                {
                    subject = $"Wniosek o Delegację {frmNumber} został rozliczony";
                }
                recipients = new List<Recipient>
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress{ Address = userEmail }
                    }

                };
                //subject = $"Wniosek o Delegację ({frmNumber}) został rozliczony";
                body = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    </head>
                    <body>
                        <div class=""header"">
                            <h1>Wniosek o Delegację został rozliczony</h1>
                        </div>
                        <div>
                            <p><h3>Wniosek o Delegację numer {frmNumber} został rozliczony.</h3></p>
                            <p>Uzasadnienie: <b>{reason}</b></p>
                            <p>Miasto: <b>{custName}</b></p>
                        </div>
                        <div>
                            <p>Kliknij w link, aby przejść do wniosku: <a href=""{_baseUrl}//delegacja/{id}?srcPage=view"">Przejdź do wniosku</a></p>
                            <p>Przejdź do aplikacji <a href=""{_baseUrl}/delegacje"">Lista wniosków</a></p>
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
                if (env.ToUpper() == "DEV")
                {
                    subject = $"##TEST APLIKACJI## Wniosek o Delegację {frmNumber} został odrzucony";
                }
                else
                {
                    subject = $"Wniosek o Delegację {frmNumber} został odrzucony";
                }
                recipients = new List<Recipient>
            {
                new Recipient
                {
                    EmailAddress = new EmailAddress{ Address = userEmail }
                }

            };
                //subject = $"Wniosek o Delegację ({frmNumber})) został odrzucony";
                body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Wniosek o Delegację</h1>
                    </div>
                    <div>
                        <p><h3>Wniosek o Delegację numer {frmNumber} został odrzucony.</h3></p>
                        <p>Wniosek dotyczy pracownika: <b>{senderName}</b></p>
                        <p>Uzasadnienie: <b>{reason}</b></p>
                        <p>Miasto: <b>{custName}</b></p>
                    </div>
                    <div>
                        <p>Kliknij w link, aby przejść do wniosku i poprawić: <a href=""{_baseUrl}/platnoscodroczona/{id}?srcPage=pracownik"">Przejdź do wniosku</a></p>
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


