using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.BackgroundJobs.Queries
{
    public class GetAllBackgroundJobQuery : IRequest<IQueryable<BackgroundJobVm>>
    {
    }

    public class GetAllBackgorundJobQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllBackgroundJobQuery, IQueryable<BackgroundJobVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<BackgroundJobVm>> Handle(GetAllBackgroundJobQuery query, CancellationToken cancellationToken)
        {
            var backgroundJobs = await _appDbContext.BackgroundJobs.ToListAsync(cancellationToken);
            var backgroundJobsVms = _mapper.Map<List<BackgroundJobVm>>(backgroundJobs);
            return backgroundJobsVms.AsQueryable();
        }
    }
}