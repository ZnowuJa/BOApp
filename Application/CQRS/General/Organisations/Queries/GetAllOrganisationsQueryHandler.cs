using System.Text.Json;

using Application.Interfaces;
using Application.ViewModels.General;

using Domain.Entities.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Queries;
public class GetAllOrganisationsQueryHandler : IRequestHandler<GetAllOrganisationsQuery, IQueryable<OrganisationVm>>
{
    private readonly IAppDbContext _appDbContext;

    public GetAllOrganisationsQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IQueryable<OrganisationVm>> Handle(GetAllOrganisationsQuery request, CancellationToken cancellationToken)
    {
        var organisations = await _appDbContext.Organisations.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
        var organisationVms = new List<OrganisationVm>();

        foreach (var organisation in organisations)
        {
            organisationVms.Add(MapToViewModel(organisation));
        }

        return organisationVms.AsQueryable();
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
            Role_SettlementsTeam = DeserializeRoles (organisation.Role_SettlementsTeam)
        };
    }

    private List<OrganisationRoleVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleVm>() : JsonSerializer.Deserialize<List<OrganisationRoleVm>>(json);
    }
}