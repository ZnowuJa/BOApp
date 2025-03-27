using Application.CQRS.CoCCQRS.Positions.Commands;
using Application.CQRS.CoCCQRS.Positions.Queries;
using Application.CQRS.ITWarehouseCQRS.Employees.Queries;
using Application.Interfaces;
using Application.ViewModels.CoC;

using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

using Quartz;

namespace Application.BackgroundJobs
{
    public class SyncPositionsJob(IAppDbContext appDbContext, IMapper mapper, IMediator mediator, IEmailService mailService, IConfiguration configuration) : IJob
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;
        public IEmailService _mailService { get; } = mailService;
        public IConfiguration _configuration { get; } = configuration;

        public async Task Execute(IJobExecutionContext context)
        {
            List<string> errorList = new();
            // Job begins each day at 9:00 AM
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            var positions = await _mediator.Send(new GetAllPositionsQuery());

            var distinctEmployeePositions = employees.Select(e => e.Position).Distinct().ToList();
            var existingPositionNames = positions.Select(p => p.Name.ToLower()).ToHashSet();

            var newPositions = new List<PositionVm>();

            foreach (var position in distinctEmployeePositions)
            {
                if (!existingPositionNames.Contains(position.ToLower()))
                {
                    var newPositionVm = new PositionVm
                    {
                        Name = position,
                        Cat = string.Empty,
                        GroupCoCId = 5,
                        GroupCoC = new GroupCoCVm()
                    };
                    errorList.Add($"New position found: {position}");
                    newPositions.Add(newPositionVm);
                }
            }

            if (newPositions.Any())
            {

                foreach (var newPosition in newPositions)
                { 
                    await _mediator.Send(new CreatePositionCommand(newPosition));
                }
            }
            else
            {
                errorList.Add("No new positions found.");
            }

            SendEmail("marcin.jarco@porscheinterauto.pl;dawid.urbaniak@porscheinterauto.pl", errorList);

        }

        private async Task SendEmail(string rcptEmail, List<string> errorList)
        {
            var _baseUrl = _configuration["BaseUrl"];
            string body = string.Empty;
            string subject = string.Empty;
            string listHTML = string.Empty;
            // Build the table of errors
            int idCounter = 1;
            listHTML = "<table style='border-collapse: collapse; width: 100%;'>"; // Start the table
            listHTML += "<tr><th style='border: 1px solid black; padding: 8px;'>ID</th><th style='border: 1px solid black; padding: 8px;'>Error</th></tr>"; // Table header

            foreach (var err in errorList)
            {
                listHTML += $"<tr><td style='border: 1px solid black; padding: 8px;'>{idCounter}</td><td style='border: 1px solid black; padding: 8px;'>{err}</td></tr>";
                idCounter++;
            }

            listHTML += "</table>"; // End the table

            var emailAddresses = rcptEmail.Split(';');
            var recipients = emailAddresses.Select(email => new Recipient
            {
                EmailAddress = new EmailAddress
                {
                    Address = email.Trim()
                }
            }).ToList();

            subject = $"Synchronizacja Stanowisk CoC";
            body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Synchronizacja Stanowisk CoC</h1>
                    </div>
                    <div>
                    </p>
                        
                        <p>Informacja:</p>
                        <p> Total errors: {errorList.Count()}</p>
                        {listHTML}
  
                        <p>Pozdrawiamy!</p>
                        <p>Twój zespół Automatyzacji!</p>
                    </div>
                    <div class=""footer"">
                        <p>© 2025 Porsche Inter Auto Polska Sp. z o.o.</p>
                    </div>
                </body>
                </html>";

            var message = new Message
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
