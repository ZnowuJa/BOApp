using System.Text.Json;

using Application.Interfaces;
using Application.ViewModels.General;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Commands;
public class UpdateOrganisationCommandHandler : IRequestHandler<UpdateOrganisationCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateOrganisationCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var existingOrganisation = await _appDbContext.Organisations
            .FirstOrDefaultAsync(o => o.Id == request.Item.Id, cancellationToken);

        if (existingOrganisation == null)
        {
            // Handle not found
            return 0;
        }

        existingOrganisation.Name = request.Item.Name;
        existingOrganisation.Make = request.Item.Make;
        existingOrganisation.Description = request.Item.Description;
        existingOrganisation.SapNumber = request.Item.SapNumber;
        existingOrganisation.Role_SalesManager = SerializeRoles(request.Item.Role_SalesManager);
        existingOrganisation.Role_ServiceManager = SerializeRoles(request.Item.Role_ServiceManager);
        existingOrganisation.Role_DealerDirector = SerializeRoles(request.Item.Role_DealerDirector);
        existingOrganisation.Role_RegionDirector = SerializeRoles(request.Item.Role_RegionDirector);
        existingOrganisation.Role_SettlementsTeam = SerializeRoles(request.Item.Role_SettlementsTeam);

        var res = await _appDbContext.SaveChangesAsync(cancellationToken);

        return existingOrganisation.Id;
    }

    private string SerializeRoles(List<OrganisationRoleVm> roles)
    {
        return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    }
}
