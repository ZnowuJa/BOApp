using System.Text.Json;

using Application.Interfaces;
using Application.ViewModels.General;

using Domain.Entities.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Queries;
public class GetOrganisationByEmpSapNumberQueryHandler : IRequestHandler<GetOrganisationByEmpSapNumberQuery, OrganisationVm>
{
    private readonly IAppDbContext _appDbContext;

    public GetOrganisationByEmpSapNumberQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<OrganisationVm> Handle(GetOrganisationByEmpSapNumberQuery request, CancellationToken cancellationToken)
    {
        
        var organisation = await _appDbContext.Organisations.FirstOrDefaultAsync(o => o.SapNumber == request.Sn, cancellationToken);

        if (organisation == null)
        {
            return null;
        }

        return MapToViewModel(organisation);
    }

    private OrganisationVm MapToViewModel(Organisation organisation)
    {
        return new OrganisationVm
        {
            Id = organisation.Id,
            Name = organisation.Name,
            Make = organisation.Make,
            Description = organisation.Description,
            SapNumber = organisation.SapNumber,
            Role_SalesManager = DeserializeRoles(organisation.Role_SalesManager),
            Role_ServiceManager = DeserializeRoles(organisation.Role_ServiceManager),
            Role_DealerDirector = DeserializeRoles(organisation.Role_DealerDirector),
            Role_RegionDirector = DeserializeRoles(organisation.Role_RegionDirector),
            Role_SettlementsTeam = DeserializeRoles(organisation.Role_SettlementsTeam)
        };
    }

    private List<OrganisationRoleVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleVm>() : JsonSerializer.Deserialize<List<OrganisationRoleVm>>(json);
    }
}
