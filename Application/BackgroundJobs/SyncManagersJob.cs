using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Administration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;

using Quartz;

namespace Application.BackgroundJobs
{
    public class SyncManagersJob(IAppDbContext appDbContext, IMapper mapper, IEmailService mailService, IConfiguration configuration) : IJob
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        public IEmailService _mailService { get; } = mailService;
        public IConfiguration _configuration { get; } = configuration;

        public async Task Execute(IJobExecutionContext context)
        {
            // Job begins each day at 9:00 AM
            var employees = await _appDbContext.Employees
                .Where(e => e.IsManager == true && e.IsActive == 1)
                .ToListAsync(context.CancellationToken);

            var existingManagerIds = new HashSet<int>(
                await _appDbContext.ManagerDeputies
                    .Select(m => m.ManagerId)
                    .ToListAsync(context.CancellationToken));

            var newManagers = employees
                .Where(e => !existingManagerIds.Contains((int)e.EnovaEmpId))
                .Select(e => new ManagerDeputy
                {
                    ManagerId = (int)e.EnovaEmpId,
                    LongName = e.LongName,
                    Deputies = "[]"
                }).ToList();

            int addedCount = newManagers.Count;

            if (addedCount > 0)
            {
                await _appDbContext.ManagerDeputies.AddRangeAsync(newManagers, context.CancellationToken);
            }

            var employeeManagerIds = new HashSet<int>(employees.Select(e => (int)e.EnovaEmpId));
            var removedCount = await _appDbContext.ManagerDeputies
                .Where(m => !employeeManagerIds.Contains(m.ManagerId))
                .ExecuteDeleteAsync(context.CancellationToken);

            if (addedCount > 0)
            {
                await _appDbContext.SaveChangesAsync(context.CancellationToken);
            }

            var message = addedCount == 0 && removedCount == 0
                ? "Lista menedżerów jest już aktualna."
                : $"Synchronizacja zakończona pomyślnie. Dodano: {addedCount}, Usunięto: {removedCount}.";

            Console.WriteLine(message);
            SendEmail("marcin.jarco@porscheinterauto.pl;dawid.urbaniak@porscheinterauto.pl", new List<string> { message });
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

            subject = $"Synchronizacja Managerów!";
            body = $@"
                <!DOCTYPE html>
                <html>
                <head>
                </head>
                <body>
                    <div class=""header"">
                        <h1>Synchronizacja Managerów</h1>
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