using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.ManagerDeputies.Queries
{
    public class GetManagerDeputyQuery(int i) : IRequest<ManagerDeputyVm>
    {
        public int Id { get; set; } = i;
    }
    public class GetManagerDeputyQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetManagerDeputyQuery, ManagerDeputyVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ManagerDeputyVm> Handle(GetManagerDeputyQuery request, CancellationToken cancellationToken)
        {
            var managerDeputy = await _appDbContext.ManagerDeputies.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
            var managerDeputyVm = _mapper.Map<ManagerDeputyVm>(managerDeputy);
            return managerDeputyVm;
        }
    }
}
