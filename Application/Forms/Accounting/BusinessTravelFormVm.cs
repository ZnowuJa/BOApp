using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Application.Interfaces;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;
using Domain.Entities.ITWarehouse;

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
    
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; } = DateTime.Now;
    public string Destination { get; set; } = string.Empty;
    public string Objective { get; set; } = string.Empty;
    public List<CountryVm> Countries { get; set; } = new(); //to keep information about flat rates and Allowance
    # region Transport
    public bool PrivateVehicle { get; set; } = false; //does trip requires private car= false;
    public int PrivateVehicleEngineSize { get; set; } = 0;//private
    public int PrivateVehicleMilage { get; set; } = 0;
    [PolishVehicleRegistration]
    public string PrivateVehicleNumber { get; set; } = string.Empty;
    public bool CompanyVehicle { get; set; } = false;
    [PolishVehicleRegistration]
    public string CompanyVehicleNumber { get; set; } = string.Empty;
    public bool PublicTransport { get; set; } = false;
    public bool PublicTransportPaid { get; set; } = false;
    # endregion
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
    #region AdvancePayment
        public bool AdvancePayment { get; set; } = false;
        public decimal? AdvancePaymentAmount { get; set; } = 0;
        public string? AdvancePaymentCurrency { get; set; } = "PLN";
        public bool? AdvancePaymentCash {get;set;} = false;
        public string? BankAccountNumber { get; set; } = string.Empty;
        // dorobic walidację numeru konta
        public DateOnly AdvancePaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        // do delegacji zagranicznej to min. 25% diety należnej wg kraju przeznaczenia i wg ilości naliczonej diety czyli czasu na jaki pracownik wypełnia delegację
    #endregion
    public DateTime? CreatedDate { get; set; } = DateTime.Now;
    public List<FormFileVm> Files { get; set; } = new();
    public List<string> ConveyanceTypes { get; set; } = new()
    {
        "Samochód służbowy", "Samochód prywatny", "Transport publiczny"
    };
    
    public List<Stage> Stages { get; set; } = new();
    public List<Accommodation> Accommodations { get; set; } = new();
    public List<Meals> Meals { get; set; } = new();
    
    
}


public class Stage()
{
    public int Id { get; set; }
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; } = DateTime.Now;
    public Decimal? Duration { get; set; } = 0;
    public bool Included { get; set; } = true;
}
public enum Objectives
{
    Szkolenie,
    Praca,
    Inne
}

public class Meals()
{
    public int StageId { get; set; }
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public decimal? Duration { get; set; } = 0;
    public decimal? BreakfastReduction { get; set; } = 0;
    public decimal? LunchReduction { get; set; } = 0;
    public decimal? DinnerReduction { get; set; } = 0;
    public int? CoveredBreakfasts { get; set; } = 0;
    public int? CoveredLunches { get; set; } = 0;
    public int? CoveredDinners { get; set; } = 0;
    public decimal Total { get; set; } = 0;
    public bool Included { get; set; } = true;
    
}
public class Accommodation()
{
    public int StageId { get; set; }
    public string CountryCode { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
    public decimal? Duration { get; set; } = 0;
    public decimal AllowanceRate { get; set; } = 0;
    public string AllowanceRateCurrency { get; set; }
    public bool HasInvoices { get; set; } = false;
    public decimal? InvoicesAmount { get; set; } = 0;
    public decimal? Total { get; set; } = 0;
    public bool Included { get; set; } = true;

}
public enum Cities
{
    Warszawa,
    Poznań,
    Kraków,
    Rybnik,
    Sosnowiec,
    Tychy,
    Katowice,
    Salzburg,
    Wiedeń
}
public enum Conveyance
{
    Samochódsłużbowy,
        Samochódprywatny
    
}
public class PrivateCar()
{
    public bool IsSelected { get; set; } = false;
    public string PlateNumber { get; set; } = string.Empty;
    public int EngineSize { get; set; } = 0;
    public int Mileage { get; set; } = 0;
}
public class PolishVehicleRegistrationAttribute : ValidationAttribute
{
    private const string Pattern = @"^[A-Z]{1,2}[A-Z0-9]{4,5}$|^[A-Z]{2}[0-9]{5}$|^[A-Z]{2}[0-9]{4}[A-Z]{1}$";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success;
        }

        var regex = new Regex(Pattern);
        if (regex.IsMatch(value.ToString()))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid Polish vehicle registration number.");
    }
}