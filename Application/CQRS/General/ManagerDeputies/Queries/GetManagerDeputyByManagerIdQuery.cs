using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.ManagerDeputies.Queries
{
    public class GetManagerDeputyByManagerIdQuery(int enovaEmpId) : IRequest<ManagerDeputyVm>
    {
        public int EnovaEmpId { get; set; } = enovaEmpId;
    }
    public class GetManagerDeputyByManagerIdQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetManagerDeputyByManagerIdQuery, ManagerDeputyVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ManagerDeputyVm> Handle(GetManagerDeputyByManagerIdQuery request, CancellationToken cancellationToken)
        {
            var managerDeputy = await _appDbContext.ManagerDeputies.FirstOrDefaultAsync(m => m.ManagerId == request.EnovaEmpId, cancellationToken);
            var managerDeputyVm = _mapper.Map<ManagerDeputyVm>(managerDeputy);

            return managerDeputyVm;
        }
    }
}