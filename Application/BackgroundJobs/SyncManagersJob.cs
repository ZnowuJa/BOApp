using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Administration;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Application.BackgroundJobs
{
    public class SyncManagersJob(IAppDbContext appDbContext, IMapper mapper) : IJob
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

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
        }
    }
}
