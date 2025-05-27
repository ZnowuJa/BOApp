using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Common;
public class Organisation : SmallAuditableEntity
{
    public string Name { get; set; }
    public string Make {  get; set; }
    public string Description { get; set; }
    public string DisplayName { get; set; }
    public string SapNumber { get; set; }
    public string Role_SalesManager { get; set; } 
    public string Role_ServiceManager { get; set; } 
    public string Role_DealerDirector { get; set; } 
    public string Role_RegionDirector { get; set; } 
    public string Role_SettlementsTeam { get; set; }
    public string Role_Cashiers { get; set; }
    public string Role_Accountants { get; set; }
    public string Role_AccountantsTeamLeader { get; set; }
    public string Role_HRSpecialists { get; set; }
    public string Role_InvestmentsDept { get; set; }
    public string Role_SourcingDept { get; set; }
    public string Role_ComplianceAssistant { get; set; }
    public string Role_ComplianceManager { get; set; }
    public string Role_Disposition { get; set; }
    public string Role_DispositionManager { get; set; }

    public string Role_ITAssetManager { get; set; }
    public string Role_ITManager { get; set; }


}
