using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Administration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.CQRS.General.ManagerDeputies.Commands
{
    public class SyncManagersCommand : IRequest<string>
    {
    }
    public class SyncManagersCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<SyncManagersCommand, string>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<string> Handle(SyncManagersCommand request, CancellationToken cancellationToken)
        {
            var employees = await _appDbContext.Employees
                .Where(e => e.IsManager)
                .ToListAsync(cancellationToken);

            var existingManagerIdsSet = new HashSet<int>(await _appDbContext.ManagerDeputies
                .Select(m => m.ManagerId)
                .ToListAsync(cancellationToken));

            var newManagers = employees
                .Where(e => !existingManagerIdsSet.Contains((int)e.EnovaEmpId))
                .Select(e => new ManagerDeputy
                {
                    ManagerId = (int)e.EnovaEmpId,
                    LongName = e.LongName,
                    Deputies = "[]"
                });

            int addedCount = newManagers.Count();

            if (addedCount > 0)
            {
                await _appDbContext.ManagerDeputies.AddRangeAsync(newManagers, cancellationToken);
            }

            var employeesManagerIds = new HashSet<int>(employees.Select(e => (int)e.EnovaEmpId));

            var removedCount = await _appDbContext.ManagerDeputies
                .Where(m => !employeesManagerIds.Contains(m.ManagerId))
                .ExecuteDeleteAsync(cancellationToken);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            var message = addedCount == 0 && removedCount == 0
                ? "Lista menedżerów jest już aktualna."
                : $"Synchronizacja zakończona pomyślnie. Dodano: {addedCount}, Usnięto: {removedCount}.";
            
            return message;
        }

    }
}
