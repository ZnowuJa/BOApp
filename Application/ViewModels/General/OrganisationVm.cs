using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Application.Mappings;

using AutoMapper;

using Domain.Entities.Common;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels.General;
public class OrganisationVm : IMapFrom<Organisation>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Make { get; set; }
    public string Description { get; set; }
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
    }

    public void Mapping(Profile profile)
    {

    }

}
