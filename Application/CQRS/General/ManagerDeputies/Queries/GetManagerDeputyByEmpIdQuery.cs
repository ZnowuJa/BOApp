using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.ManagerDeputies.Queries
{
    public class GetManagerDeputyByEmpIdQuery(int enovaEmpId) : IRequest<ManagerDeputyVm>
    {
        public int EnovaEmpId { get; set; } = enovaEmpId;
    }
    public class GetManagerDeputyByEmpIdQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetManagerDeputyByEmpIdQuery, ManagerDeputyVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ManagerDeputyVm> Handle(GetManagerDeputyByEmpIdQuery request, CancellationToken cancellationToken)
        {
            var managerId = await _appDbContext.Employees.Where(e => e.EnovaEmpId == request.EnovaEmpId).Select(p => p.ManagerId).FirstOrDefaultAsync();
            var managerDeputy = await _appDbContext.ManagerDeputies.FirstOrDefaultAsync(m => m.ManagerId == managerId, cancellationToken);
            var managerDeputyVm = _mapper.Map<ManagerDeputyVm>(managerDeputy);

            return managerDeputyVm;
        }
    }
}