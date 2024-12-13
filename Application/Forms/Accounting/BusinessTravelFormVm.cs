using Application.Interfaces;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;

namespace Application.Forms.Accounting;

public class BusinessTravelFormVm : IFormVm
{
    # region FromTemplate
    // Properties from FormTemplate
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "Delegacja";
    public string Description { get; set; } = "Formularz do rozliczania Delegacji pracowniczych.";
    public string FolderName { get; set; } = "BusinessTravels";
    public List<FormFileVm> FormFiles { get; set; } = new();
    public string NumberPrefix { get; set; } = "DEL";
    public string Status { get; set; } = "Rejestracja";
    public string? Number { get; set; } = string.Empty;
    public List<string> Statuses { get; set; } = new();
    public int WorkflowTemplateId { get; set; } = 5;
    # endregion
    
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public string Destination { get; set; } = string.Empty;
    public string Objective { get; set; } = string.Empty;

    public List<BusinessTripStage> Stages { get; set; } = new();//stage is a list of stages of trip - BusinessTripStage. 
    public List<CountryVm> Countries { get; set; } = new(); //to keep information about flat rates and Allowance
    public bool PrivateVehicle { get; set; } = false; //does trip requires private car= false;
    public string EngineSize { get; set; } = string.Empty;//private
    public string CompanyVehicle { get; set; } = string.Empty;
    
    public string VehicleNumber { get; set; } = string.Empty;
    public DateTime? CreatedDate { get; set; } = DateTime.Now;
    
    public List<FormFileVm> Files { get; set; } = new();

    #region Approvers&Approvals
    public string EmployeeName { get; set; } = string.Empty;
    public string EnovaEmpId { get; set; } = string.Empty;
    public List<Approval>? Approvals { get; set; } = new();
    public List<OrganisationRoleForFormVm> Level1Approvers { get; set; } = new(); // przełożony wniosek
    public List<OrganisationRoleForFormVm> Level2Approvers { get; set; } = new(); // przełożony rozliczenie
    public List<OrganisationRoleForFormVm> Level3Approvers { get; set; } = new(); // dyrektor salonu
    public List<OrganisationRoleForFormVm> Level4Approvers { get; set; } = new(); // Księgowość
    public List<OrganisationRoleForFormVm> Level5Approvers { get; set; } = new(); // Księgowość TeamLeader
    public string LVL1_EnovaEmpId { get; set; } = string.Empty;
    public string LVL1_EmployeeName { get; set; } = string.Empty;
    public string LVL2_EnovaEmpId { get; set; } = string.Empty;
    public string LVL2_EmployeeName { get; set; } = string.Empty;
    public string LVL3_EnovaEmpId { get; set; } = string.Empty;
    public string LVL3_EmployeeName { get; set; } = string.Empty;
    public string LVL4_EnovaEmpId { get; set; } = string.Empty;
    public string LVL4_EmployeeName { get; set; } = string.Empty;
    public string LVL5_EnovaEmpId { get; set; } = string.Empty;
    public string LVL5_EmployeeName { get; set; } = string.Empty;

    #endregion
  
    
    
    
}