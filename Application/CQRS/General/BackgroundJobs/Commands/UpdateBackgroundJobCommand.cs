using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.BackgroundJobs.Commands
{
    public class UpdateBackgroundJobCommand(BackgroundJobVm backgroundJob) : IRequest<int>
    {
        public BackgroundJobVm BackgroundJob { get; set; } = backgroundJob;
    }

    public class UpdateBackgroundJobCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<UpdateBackgroundJobCommand, int>
    {
        private readonly IAppDbContext _appDbContext = context;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(UpdateBackgroundJobCommand request, CancellationToken cancellationToken)
        {
            var backgroundJob = await _appDbContext.BackgroundJobs.FirstOrDefaultAsync(g => g.Id == request.BackgroundJob.Id, cancellationToken);
            _mapper.Map(request.BackgroundJob, backgroundJob);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return backgroundJob.Id;
        }
    }
}