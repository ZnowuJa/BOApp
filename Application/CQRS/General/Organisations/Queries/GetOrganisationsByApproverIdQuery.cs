using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Queries;
public class GetOrganisationsByApproverIdQuery : IRequest<IQueryable<OrganisationVm>>
{
    public GetOrganisationsByApproverIdQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}

//
//
// Title of this query is a bit misleading
// it looks like it searches for Org by EmpId but 
// it searches Org where EmpId is in any Role
//
//

public class GetOrganisationsByApproverIdQueryHandler : IRequestHandler<GetOrganisationsByApproverIdQuery, IQueryable<OrganisationVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetOrganisationsByApproverIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
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
        var organisationVms = _mapper.Map<List<OrganisationVm>>(organisations);
        //var organisations = await _appDbContext.Organisations.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
        //var organisationVms = new List<OrganisationVm>();

        //foreach (var organisation in organisations)
        //{
        //    organisationVms.Add(MapToViewModel(organisation));
        //}

        return organisationVms.AsQueryable();
    }

    //private OrganisationVm MapToViewModel(Organisation organisation)
    //{
    //    return new OrganisationVm
    //    {
    //        Id = organisation.Id,
    //        Title = organisation.Title,
    //        Make = organisation.Make,
    //        Description = organisation.Description,
    //        SapNumber = organisation.SapNumber,
    //        Role_SalesManager = DeserializeRoles(organisation.Role_SalesManager),
    //        Role_ServiceManager = DeserializeRoles(organisation.Role_ServiceManager),
    //        Role_DealerDirector = DeserializeRoles(organisation.Role_DealerDirector),
    //        Role_RegionDirector = DeserializeRoles(organisation.Role_RegionDirector),
    //        Role_SettlementsTeam = DeserializeRoles(organisation.Role_SettlementsTeam),
    //        Role_Cashiers = DeserializeRoles(organisation.Role_Cashiers),
    //        Role_Accountants = DeserializeRoles(organisation.Role_Accountants),
    //        Role_AccountantsTeamLeader = DeserializeRoles(organisation.Role_AccountantsTeamLeader),
    //        Role_HRSpecialists = DeserializeRoles(organisation.Role_HRSpecialists),
    //        Role_InvestmentsDept = DeserializeRoles(organisation.Role_InvestmentsDept),
    //        Role_SourcingDept = DeserializeRoles(organisation.Role_SourcingDept),
    //        Role_ComplianceAssistant = DeserializeRoles(organisation.Role_ComplianceAssistant),
    //        Role_ComplianceManager = DeserializeRoles(organisation.Role_ComplianceManager)
    //    };
    //}

    //private List<OrganisationRoleVm> DeserializeRoles(string json)
    //{
    //    return string.IsNullOrEmpty(json) ? new List<OrganisationRoleVm>() : JsonSerializer.Deserialize<List<OrganisationRoleVm>>(json);
    //}
}