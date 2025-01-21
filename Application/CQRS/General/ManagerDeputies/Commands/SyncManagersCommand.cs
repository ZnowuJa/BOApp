using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Administration;
using MediatR;
using Microsoft.EntityFrameworkCore;


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
            // Pobiera pracowników, którzy są managerami
            var employees = await _appDbContext.Employees
                .Where(e => e.IsManager == true)
                .ToListAsync(cancellationToken);

            // Pobiera istniejących managerów w ManagerDeputies
            var existingManagerIds = await _appDbContext.ManagerDeputies
                .Select(m => m.ManagerId)
                .ToListAsync(cancellationToken);

            int addedCount = 0;

            // Dodaj nowych managerów
            foreach (var employee in employees)
            {
                if (!existingManagerIds.Contains((int)employee.EnovaEmpId))
                {
                    _appDbContext.ManagerDeputies.Add(new ManagerDeputy
                    {
                        ManagerId = (int)employee.EnovaEmpId,
                        LongName = employee.LongName,
                        Deputies = "[]"
                    });
                    addedCount++;
                }
            }

            // Usuwa managerów, którzy nie są już managerami
            var employeesManagerIds = employees.Select(e => e.EnovaEmpId).ToList();
            var managersToRemove = await _appDbContext.ManagerDeputies
                .Where(m => !employeesManagerIds.Contains(m.ManagerId))
                .ToListAsync(cancellationToken);

            int removedCount = managersToRemove.Count;

            _appDbContext.ManagerDeputies.RemoveRange(managersToRemove);

            // Zapisuje zmiany w bazie danych
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var message = addedCount == 0 && removedCount == 0
                ? "Lista menedżerów jest już aktualna."
                : $"Synchronizacja zakończona pomyślnie. Dodano: {addedCount}, Usnięto: {removedCount}.";

            return message;
        }

    }
}
