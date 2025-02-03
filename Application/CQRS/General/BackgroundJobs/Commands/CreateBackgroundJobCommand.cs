using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Entities.Administration;
using MediatR;


namespace Application.CQRS.General.BackgroundJobs.Commands
{
    public class CreateBackgroundJobCommand(BackgroundJobVm backgroundJob) : IRequest<int>
    {
        public BackgroundJobVm BackgroundJob { get; set; } = backgroundJob;
    }

    public class CreateBackgroundJobCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<CreateBackgroundJobCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateBackgroundJobCommand request, CancellationToken cancellationToken)
        {
            var backgroundJob = _mapper.Map<BackgroundJob>(request.BackgroundJob);
            _context.BackgroundJobs.Add(backgroundJob);
            await _context.SaveChangesAsync(cancellationToken);
            return backgroundJob.Id;
        }
    }
}
