using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;

using Domain.Entities.Common;

using MediatR;

namespace Application.CQRS.General.Organisations.Commands;
public class CreateOrganisationCommand : IRequest<int>
{
    public OrganisationVm Item { get; set; }

    public CreateOrganisationCommand(OrganisationVm item)
    {
        Item = item;
    }
}


public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateOrganisationCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        //var organisation = new Organisation
        //{
        //    Title = request.Item.Title,
        //    Make = request.Item.Make,
        //    Description = request.Item.Description,
        //    SapNumber = request.Item.SapNumber,
        //    Role_SalesManager = SerializeRoles(request.Item.Role_SalesManager),
        //    Role_ServiceManager = SerializeRoles(request.Item.Role_ServiceManager),
        //    Role_DealerDirector = SerializeRoles(request.Item.Role_DealerDirector),
        //    Role_RegionDirector = SerializeRoles(request.Item.Role_RegionDirector),
        //    Role_SettlementsTeam = SerializeRoles(request.Item.Role_SettlementsTeam),
        //    Role_Cashiers = SerializeRoles(request.Item.Role_Cashiers),
        //    Role_Accountants = SerializeRoles(request.Item.Role_Accountants),
        //    Role_AccountantsTeamLeader = SerializeRoles(request.Item.Role_AccountantsTeamLeader),
        //    Role_HRSpecialists = SerializeRoles(request.Item.Role_HRSpecialists),
        //    Role_InvestmentsDept = SerializeRoles(request.Item.Role_InvestmentsDept),
        //    Role_SourcingDept = SerializeRoles(request.Item.Role_SourcingDept),
        //    Role_ComplianceAssistant = SerializeRoles(request.Item.Role_ComplianceAssistant),
        //    Role_ComplianceManager = SerializeRoles(request.Item.Role_ComplianceManager)
        //};

        var organisation = _mapper.Map<Organisation>(request.Item);

        _appDbContext.Organisations.Add(organisation);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return organisation.Id;
    }

    //private string SerializeRoles(List<OrganisationRoleVm> roles)
    //{
    //    return roles == null || roles.Count == 0 ? null : JsonSerializer.Serialize(roles);
    //}
}

