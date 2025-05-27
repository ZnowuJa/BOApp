using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Queries;
public class GetOrganisationByEmpSapNumberQuery : IRequest<OrganisationVm>
{
    public string Sn { get; set; }
    public GetOrganisationByEmpSapNumberQuery(string sn)
    {
        Sn = sn;
    }
}

public class GetOrganisationByEmpSapNumberQueryHandler : IRequestHandler<GetOrganisationByEmpSapNumberQuery, OrganisationVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetOrganisationByEmpSapNumberQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<OrganisationVm> Handle(GetOrganisationByEmpSapNumberQuery request, CancellationToken cancellationToken)
    {

        var organisation = await _appDbContext.Organisations.FirstOrDefaultAsync(o => o.SapNumber == request.Sn && o.StatusId == 1, cancellationToken);
        var organisationVm = _mapper.Map<OrganisationVm>(organisation);
        //if (organisation == null)
        //{
        //    return null;
        //}

        return organisationVm;
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
