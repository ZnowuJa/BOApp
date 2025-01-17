using System.Text.Json;
using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Commands;
public class UpdateOrganisationCommand : IRequest<int>
{
    public OrganisationVm Item { get; set; }

    public UpdateOrganisationCommand(OrganisationVm organisationVm)
    {
        Item = organisationVm;
    }
}



public class UpdateOrganisationCommandHandler : IRequestHandler<UpdateOrganisationCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UpdateOrganisationCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var existingOrganisation = await _appDbContext.Organisations
            .FirstOrDefaultAsync(o => o.Id == request.Item.Id, cancellationToken);
        _mapper.Map(request.Item, existingOrganisation);

        _appDbContext.Organisations.Update(existingOrganisation);

        var res = await _appDbContext.SaveChangesAsync(cancellationToken);

        return existingOrganisation.Id;
    }

    private string SerializeRoles(List<OrganisationRoleVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }
}

//if (existingOrganisation == null)
//{
//    // Handle not found
//    return 0;
//}
//existingOrganisation = _mapper.Map<Organisation>(request.Item);


//existingOrganisation.Title = request.Item.Title;
//existingOrganisation.Make = request.Item.Make;
//existingOrganisation.Description = request.Item.Description;
//existingOrganisation.SapNumber = request.Item.SapNumber;
//existingOrganisation.Role_SalesManager = SerializeRoles(request.Item.Role_SalesManager);
//existingOrganisation.Role_ServiceManager = SerializeRoles(request.Item.Role_ServiceManager);
//existingOrganisation.Role_DealerDirector = SerializeRoles(request.Item.Role_DealerDirector);
//existingOrganisation.Role_RegionDirector = SerializeRoles(request.Item.Role_RegionDirector);
//existingOrganisation.Role_SettlementsTeam = SerializeRoles(request.Item.Role_SettlementsTeam);
//existingOrganisation.Role_Cashiers = SerializeRoles(request.Item.Role_Cashiers);
//existingOrganisation.Role_Accountants = SerializeRoles(request.Item.Role_Accountants);
//existingOrganisation.Role_AccountantsTeamLeader = SerializeRoles(request.Item.Role_AccountantsTeamLeader);
//existingOrganisation.Role_HRSpecialists = SerializeRoles(request.Item.Role_HRSpecialists);
//existingOrganisation.Role_InvestmentsDept = SerializeRoles(request.Item.Role_InvestmentsDept);
//existingOrganisation.Role_SourcingDept = SerializeRoles(request.Item.Role_SourcingDept);
//existingOrganisation.Role_ComplianceAssistant = SerializeRoles(request.Item.Role_ComplianceAssistant);
//existingOrganisation.Role_ComplianceManager = SerializeRoles(request.Item.Role_ComplianceManager);