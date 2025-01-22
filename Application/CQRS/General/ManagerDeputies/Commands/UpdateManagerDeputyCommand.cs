using Application.CQRS.General.ManagerDeputies.Commands;
using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Application.CQRS.General.ManagerDeputies.Commands
{
    public class UpdateManagerDeputyCommand(ManagerDeputyVm managerDeputyVm) : IRequest<int>
    {
        public ManagerDeputyVm Item { get; set; } = managerDeputyVm;
    }
}

public class UpdateManagerDeputyCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<UpdateManagerDeputyCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(UpdateManagerDeputyCommand request, CancellationToken cancellationToken)
    {
        var existingManagerDeputy = await _appDbContext.ManagerDeputies
            .FirstOrDefaultAsync(o => o.Id == request.Item.Id, cancellationToken);

        _mapper.Map(request.Item, existingManagerDeputy);
        _appDbContext.ManagerDeputies.Update(existingManagerDeputy);
        var res = await _appDbContext.SaveChangesAsync(cancellationToken);
        return existingManagerDeputy.Id;
    }
    private string SerializeRoles(List<OrganisationRoleVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }
}