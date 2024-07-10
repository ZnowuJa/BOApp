using System.Text.Json;

using Application.Interfaces;
using Application.ViewModels.General;

using Domain.Entities.Common;

using MediatR;

namespace Application.CQRS.General.Organisations.Commands;
public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public CreateOrganisationCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organisation = new Organisation
        {
            Name = request.Item.Name,
            Make = request.Item.Make,
            Description = request.Item.Description,
            SapNumber = request.Item.SapNumber,
            Role_SalesManager = SerializeRoles(request.Item.Role_SalesManager),
            Role_ServiceManager = SerializeRoles(request.Item.Role_ServiceManager),
            Role_DealerDirector = SerializeRoles(request.Item.Role_DealerDirector),
            Role_RegionDirector = SerializeRoles(request.Item.Role_RegionDirector),
            Role_SettlementsTeam = SerializeRoles(request.Item.Role_SettlementsTeam)
        };

        _appDbContext.Organisations.Add(organisation);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return organisation.Id;
    }

    private string SerializeRoles(List<OrganisationRoleVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }
}
