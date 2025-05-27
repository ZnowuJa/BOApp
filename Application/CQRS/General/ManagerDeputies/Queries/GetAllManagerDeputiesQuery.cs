using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.ManagerDeputies.Queries
{
    public class GetAllManagerDeputiesQuery : IRequest<IQueryable<ManagerDeputyVm>>
    {
    }
    public class GetAllManagerDeputiesQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllManagerDeputiesQuery, IQueryable<ManagerDeputyVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<ManagerDeputyVm>> Handle(GetAllManagerDeputiesQuery request, CancellationToken cancellationToken)
        {
            var managerDeputies = await _appDbContext.ManagerDeputies.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
            var managerDeputiesVms = _mapper.Map<List<ManagerDeputyVm>>(managerDeputies);
            return managerDeputiesVms.AsQueryable();
        }
    }
}