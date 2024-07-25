using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.General;

using Domain.Entities.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;



//
//
// Name of this query is a bit misleading
// it looks like it searches for Org by EmpId but 
// it searches Org where EmpId is in any Role
//
//

namespace Application.CQRS.General.Organisations.Queries;
public class GetOrganisationsByApproverIdQueryHandler : IRequestHandler<GetOrganisationsByApproverIdQuery, IQueryable<OrganisationVm>>
{
    private readonly IAppDbContext _appDbContext;

    public GetOrganisationsByApproverIdQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IQueryable<OrganisationVm>> Handle(GetOrganisationsByApproverIdQuery request, CancellationToken cancellationToken)
    {
        var query = $@"
        SELECT * 
        FROM Organisations
        WHERE 
            EXISTS (
                SELECT 1
                FROM OPENJSON(Role_SalesManager)
                WITH (EmpId int '$.EmpId') AS json
                WHERE json.EmpId = {request.Id}
            )
            OR 
            EXISTS (
                SELECT 1
                FROM OPENJSON(Role_RegionDirector)
                WITH (EmpId int '$.EmpId') AS json
                WHERE json.EmpId = {request.Id}
            )";

        //var empIdParameter = new SqlParameter("@empId", request.Id);

        var organisations = await _appDbContext.Organisations.FromSqlRaw(query).ToListAsync(cancellationToken);
        //var organisations = await _appDbContext.Organisations.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
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
            Role_SettlementsTeam = DeserializeRoles(organisation.Role_SettlementsTeam)
        };
    }

    private List<OrganisationRoleVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleVm>() : JsonSerializer.Deserialize<List<OrganisationRoleVm>>(json);
    }
}