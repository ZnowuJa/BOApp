using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.BackgroundJobs.Queries
{
    public class GetBackgroundJobByIdQuery(int i) : IRequest<BackgroundJobVm>
    {
        public int Id { get; set; } = i;
    }

    public class GetBackgroundJobByIdQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetBackgroundJobByIdQuery, BackgroundJobVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<BackgroundJobVm> Handle(GetBackgroundJobByIdQuery request, CancellationToken cancellationToken)
        {
            var backgroundJobs = await _appDbContext.BackgroundJobs.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
            var backgroundJobsVms = _mapper.Map<BackgroundJobVm>(backgroundJobs);
            return backgroundJobsVms;
        }
    }
}