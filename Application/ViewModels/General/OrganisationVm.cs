using System.Text.Json;
using Application.Forms.Accounting;
using Application.Mappings;

using AutoMapper;

using Domain.Entities.Common;

namespace Application.ViewModels.General;
public class OrganisationVm : IMapFrom<Organisation>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Make { get; set; }
    public string Description { get; set; }
    public string DisplayName { get; set; }
    public string SapNumber { get; set; }
    public List<OrganisationRoleVm> Role_SalesManager { get; set; }
    public List<OrganisationRoleVm> Role_ServiceManager { get; set; }
    public List<OrganisationRoleVm> Role_DealerDirector { get; set; }
    public List<OrganisationRoleVm> Role_RegionDirector { get; set; }
    public List<OrganisationRoleVm> Role_SettlementsTeam { get; set; }
    public List<OrganisationRoleVm> Role_Cashiers { get; set; }
    public List<OrganisationRoleVm> Role_Accountants { get; set; }
    public List<OrganisationRoleVm> Role_AccountantsTeamLeader { get; set; }
    public List<OrganisationRoleVm> Role_HRSpecialists { get; set; }
    public List<OrganisationRoleVm> Role_InvestmentsDept {  get; set; }
    public List<OrganisationRoleVm> Role_SourcingDept { get; set; }
    public List<OrganisationRoleVm> Role_ComplianceAssistant { get; set; }
    public List<OrganisationRoleVm> Role_ComplianceManager { get; set; }
    public List<OrganisationRoleVm> Role_Disposition { get; set; }
    public List<OrganisationRoleVm> Role_DispositionManager { get; set; }
    public List<OrganisationRoleVm> Role_ITAssetManager { get; set; }
    public List<OrganisationRoleVm> Role_ITManager { get; set; }

    public void InitializeRoles()
    {
        Role_SalesManager = Role_SalesManager ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_ServiceManager = Role_ServiceManager ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_DealerDirector = Role_DealerDirector ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_RegionDirector = Role_RegionDirector ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_SettlementsTeam = Role_SettlementsTeam ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_Cashiers = Role_Cashiers ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_Accountants = Role_Accountants ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_AccountantsTeamLeader = Role_AccountantsTeamLeader ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_HRSpecialists = Role_HRSpecialists ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_InvestmentsDept = Role_InvestmentsDept ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_SourcingDept = Role_SourcingDept ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_ComplianceAssistant = Role_ComplianceAssistant ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_ComplianceManager = Role_ComplianceManager ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_Disposition = Role_Disposition ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_DispositionManager = Role_DispositionManager ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_ITAssetManager = Role_ITAssetManager ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
        Role_ITManager = Role_ITManager ?? new List<OrganisationRoleVm> { new OrganisationRoleVm() };
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Organisation, LocationVm>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.SapNumber, opt => opt.MapFrom(src => src.SapNumber));
        
        profile.CreateMap<Organisation, OrganisationVm>()
            .ForMember(dest => dest.Role_SalesManager, opt => opt.MapFrom(src => DeserializeRoles(src.Role_SalesManager)))
            .ForMember(dest => dest.Role_ServiceManager, opt => opt.MapFrom(src => DeserializeRoles(src.Role_ServiceManager)))
            .ForMember(dest => dest.Role_DealerDirector, opt => opt.MapFrom(src => DeserializeRoles(src.Role_DealerDirector)))
            .ForMember(dest => dest.Role_RegionDirector, opt => opt.MapFrom(src => DeserializeRoles(src.Role_RegionDirector)))
            .ForMember(dest => dest.Role_SettlementsTeam, opt => opt.MapFrom(src => DeserializeRoles(src.Role_SettlementsTeam)))
            .ForMember(dest => dest.Role_Cashiers, opt => opt.MapFrom(src => DeserializeRoles(src.Role_Cashiers)))
            .ForMember(dest => dest.Role_Accountants, opt => opt.MapFrom(src => DeserializeRoles(src.Role_Accountants)))
            .ForMember(dest => dest.Role_AccountantsTeamLeader, opt => opt.MapFrom(src => DeserializeRoles(src.Role_AccountantsTeamLeader)))
            .ForMember(dest => dest.Role_HRSpecialists, opt => opt.MapFrom(src => DeserializeRoles(src.Role_HRSpecialists)))
            .ForMember(dest => dest.Role_InvestmentsDept, opt => opt.MapFrom(src => DeserializeRoles(src.Role_InvestmentsDept)))
            .ForMember(dest => dest.Role_SourcingDept, opt => opt.MapFrom(src => DeserializeRoles(src.Role_SourcingDept)))
            .ForMember(dest => dest.Role_ComplianceAssistant, opt => opt.MapFrom(src => DeserializeRoles(src.Role_ComplianceAssistant)))
            .ForMember(dest => dest.Role_ComplianceManager, opt => opt.MapFrom(src => DeserializeRoles(src.Role_ComplianceManager)))
            .ForMember(dest => dest.Role_Disposition, opt => opt.MapFrom(src => DeserializeRoles(src.Role_Disposition)))
            .ForMember(dest => dest.Role_DispositionManager, opt => opt.MapFrom(src => DeserializeRoles(src.Role_DispositionManager)))
            .ForMember(dest => dest.Role_ITAssetManager, opt => opt.MapFrom(src => DeserializeRoles(src.Role_ITAssetManager)))
            .ForMember(dest => dest.Role_ITManager, opt => opt.MapFrom(src => DeserializeRoles(src.Role_ITManager)));


        profile.CreateMap<OrganisationVm, Organisation>()
            .ForMember(dest => dest.Role_SalesManager, opt => opt.MapFrom(src => SerializeRoles(src.Role_SalesManager)))
            .ForMember(dest => dest.Role_ServiceManager, opt => opt.MapFrom(src => SerializeRoles(src.Role_ServiceManager)))
            .ForMember(dest => dest.Role_DealerDirector, opt => opt.MapFrom(src => SerializeRoles(src.Role_DealerDirector)))
            .ForMember(dest => dest.Role_RegionDirector, opt => opt.MapFrom(src => SerializeRoles(src.Role_RegionDirector)))
            .ForMember(dest => dest.Role_SettlementsTeam, opt => opt.MapFrom(src => SerializeRoles(src.Role_SettlementsTeam)))
            .ForMember(dest => dest.Role_Cashiers, opt => opt.MapFrom(src => SerializeRoles(src.Role_Cashiers)))
            .ForMember(dest => dest.Role_Accountants, opt => opt.MapFrom(src => SerializeRoles(src.Role_Accountants)))
            .ForMember(dest => dest.Role_AccountantsTeamLeader, opt => opt.MapFrom(src => SerializeRoles(src.Role_AccountantsTeamLeader)))
            .ForMember(dest => dest.Role_HRSpecialists, opt => opt.MapFrom(src => SerializeRoles(src.Role_HRSpecialists)))
            .ForMember(dest => dest.Role_InvestmentsDept, opt => opt.MapFrom(src => SerializeRoles(src.Role_InvestmentsDept)))
            .ForMember(dest => dest.Role_SourcingDept, opt => opt.MapFrom(src => SerializeRoles(src.Role_SourcingDept)))
            .ForMember(dest => dest.Role_ComplianceAssistant, opt => opt.MapFrom(src => SerializeRoles(src.Role_ComplianceAssistant)))
            .ForMember(dest => dest.Role_ComplianceManager, opt => opt.MapFrom(src => SerializeRoles(src.Role_ComplianceManager)))
            .ForMember(dest => dest.Role_Disposition, opt => opt.MapFrom(src => SerializeRoles(src.Role_Disposition)))
            .ForMember(dest => dest.Role_DispositionManager, opt => opt.MapFrom(src => SerializeRoles(src.Role_DispositionManager)))
            .ForMember(dest => dest.Role_ITAssetManager, opt => opt.MapFrom(src => SerializeRoles(src.Role_ITAssetManager)))
            .ForMember(dest => dest.Role_ITManager, opt => opt.MapFrom(src => SerializeRoles(src.Role_ITManager)));

    }

    private string SerializeRoles(List<OrganisationRoleVm> roles)
    {
        return roles == null || roles.Count == 0 ? string.Empty : JsonSerializer.Serialize(roles);
    }

    private List<OrganisationRoleVm> DeserializeRoles(string json)
    {
        return string.IsNullOrEmpty(json) ? new List<OrganisationRoleVm>() : JsonSerializer.Deserialize<List<OrganisationRoleVm>>(json);
    }

}
