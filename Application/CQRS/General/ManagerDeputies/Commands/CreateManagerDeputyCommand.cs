using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Entities.Administration;
using MediatR;


namespace Application.CQRS.General.ManagerDeputies.Commands
{
    public class CreateManagerDeputyCommand(ManagerDeputyVm item) : IRequest<int>
    {
        public ManagerDeputyVm Item {get; set; } = item;
    }
    public class CreateManagerDeputyCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<CreateManagerDeputyCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateManagerDeputyCommand request, CancellationToken cancellationToken)
        {
            var managerDeputy = _mapper.Map<ManagerDeputy>(request.Item);

            _appDbContext.ManagerDeputies.Add(managerDeputy);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return managerDeputy.Id;
        }
    }
}