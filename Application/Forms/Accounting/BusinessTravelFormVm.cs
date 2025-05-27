using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.AdHocJobs;
using Application.Forms.Accounting.BuisnessTravelSmallClasses;
using Application.Interfaces;
using Application.Mappings;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms.Accounting;

namespace Application.Forms.Accounting;
public class BusinessTravelFormVm : IMapFrom<BusinessTravelForm>, IFormAccounting
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
    public string? Number { get; set; } = "brak numeru";
    //public List<string> BusinessTravelStatuses { get; set; } = new();
    public int WorkflowTemplateId { get; set; } = 5;
    # endregion
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; } 
    
    public string? Destination { get; set; } = string.Empty;
    
    public string Objective { get; set; } = string.Empty;
    
    public List<CountryVm> Countries { get; set; } = new(); //to keep information about flat rates and Allowance
    
    public CountryVm? DestinationCountry { get; set; } = new();
    public string? DestinationCountryCurrency { get; set; } = string.Empty;
    #region Transport
        public string Transportation { get; set; } = string.Empty;
        public bool PrivateVehicle { get; set; } = false; //does trip requires private car= false;
        //public int PrivateVehicleEngineSize { get; set; } = 0;//private
        //public string PrivateVehicleEngineSize {
        //    get => MileageRegister.PrivateCarEngineSize;
        //    set => MileageRegister.PrivateCarEngineSize = value;
        //} 

        public int PrivateVehicleMilage { get; set; } = 0;
        
        //public string PrivateVehicleNumber {
        //    get => MileageRegister.PrivateCarRegistration; 
        //    set => MileageRegister.PrivateCarRegistration = value; 
        //}
        //public decimal PrivateCarRateFactor { get; set; } = 0;
        public bool CompanyVehicle { get; set; } = false;
        
        
        public string CompanyVehicleNumber { get; set; } = string.Empty;
        public bool PublicTransport { get; set; } = false;
        public bool PublicTransportPaid { get; set; } = false;
        public MileageRegister MileageRegister { get; set; } = new();
    #endregion
    #region Approvers&Approvals

        public string? OrganisationSapNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string EnovaEmpId { get; set; } = string.Empty;
        public CostCenterVm FormCostCenter { get; set; } = new CostCenterVm();
        public LocationVm FormCostLocation { get; set; } = new ();
        public List<SapCostCenterVm> FormCostCenters = new();
        public List<ApprovalVm>? Approvals { get; set; } = new();
        public List<RejectReason> RejectReasons { get; set; } = new();
    #region ApproversDetails
        public List<OrganisationRoleForFormVm> Level1Approvers { get; set; } = new(); // przełożony etapy: AprobataL1, AprobataL11
        public List<OrganisationRoleForFormVm> Level2Approvers { get; set; } = new(); // Kasa etapy: ZaliczkaKasa, RozliczenieKasa
        public List<OrganisationRoleForFormVm> Level3Approvers { get; set; } = new(); // Księgowość etapy: ZaliczkaKsiegowosc, Ksiegowosc
        public List<OrganisationRoleForFormVm> Level4Approvers { get; set; } = new(); // Księgowość TeamLeader etapy: ZaliczkaKsiegowoscTL, KsiegowoscTL
        public List<OrganisationRoleForFormVm> Level5Approvers { get; set; } = new(); // drugi przełożony etapy: AprobataL12
        public List<OrganisationRoleForFormVm> Level6Approvers { get; set; } = new(); // Na razie nie wykorzystywany
    
        public string LVL1_EnovaEmpId { get; set; } = string.Empty;
        public string LVL1_EmployeeName { get; set; } = string.Empty; // manager of user
        public string LVL2_EnovaEmpId { get; set; } = string.Empty;
        public string LVL2_EmployeeName { get; set; } = string.Empty;
        public string LVL3_EnovaEmpId { get; set; } = string.Empty;
        public string LVL3_EmployeeName { get; set; } = string.Empty;

        public string LVL4_EnovaEmpId { get; set; } = string.Empty;
        public string LVL4_EmployeeName { get; set; } = string.Empty;
        public string LVL5_EnovaEmpId { get; set; } = string.Empty;
        public string LVL5_EmployeeName { get; set; } = string.Empty;
        public string LVL6_EnovaEmpId { get; set; } = string.Empty;
        public string LVL6_EmployeeName { get; set; } = string.Empty;
        //public string LVL1_EmployeeEmail { get; set; } = string.Empty;
        //public string LVL2_EmployeeEmail { get; set; } = string.Empty;
        //public string LVL3_EmployeeEmail { get; set; } = string.Empty;
        //public string LVL4_EmployeeEmail { get; set; } = string.Empty;
        //public string LVL5_EmployeeEmail { get; set; } = string.Empty;
        //public string LVL6_EmployeeEmail { get; set; } = string.Empty;

    #endregion
    public string? RejectReason { get; set; } = string.Empty;

    #endregion
    #region AdvancePayment
        public bool AdvancePayment { get; set; } = false;
        public decimal? AdvancePaymentAmount { get; set; } = 0;
        // do delegacji zagranicznej to min. 25% diety należnej wg kraju przeznaczenia i wg ilości naliczonej diety czyli czasu na jaki pracownik wypełnia delegację
        public string? AdvancePaymentCurrency { get; set; } = "PLN";
        public bool AdvancePaymentCash {get;set;} = false;
        public string? BankAccountNumber { get; set; } = string.Empty;

    public DateOnly? AdvancePaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? CashPayoutNumber { get; set; } = string.Empty; //payout to zaliczka
        public string? PayoutCashierEmpId { get; set; } = string.Empty;
        [JsonIgnore] public EmployeeVm? PayoutCashier { get; set; } = new();


        //public decimal? ReceiptPaymentAmount { get; set; } = 0; 
        // do delegacji zagranicznej to min. 25% diety należnej wg kraju przeznaczenia i wg ilości naliczonej diety czyli czasu na jaki pracownik wypełnia delegację
        public string? ReceiptPaymentCurrency { get; set; } = "PLN";
        public bool ReceiptPaymentCash { get; set; } = false;

        public DateOnly? ReceiptPaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? CashReceiptNumber { get; set; } = string.Empty; //receipt to rozliczenie
        public string? ReceiptBankAccountNumber { get; set; } = string.Empty;
        public string? ReceiptCashierEmpId { get; set; } = string.Empty;
        
        [JsonIgnore] public EmployeeVm ReceiptCashier { get; set; } = new();
        public decimal CurrencyExchangeRate { get; set; } 
        public DateOnly CurrencyExchangeRateDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    #endregion
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public List<Stage> Stages { get; set; } = new();
    public List<Accommodation> Accommodations { get; set; } = new();
    public List<Meals> Meals { get; set; } = new();
    public List<DailyMeal> DailyMeals { get; set; } = new();
    public List<LocalTravel> LocalTravels { get; set; } = new();
    public Transit Transit { get; set; } = new();
    public List<Bill> Bills { get; set; } = new();

    public BankTransferMapping? BTMappingAdvancePayment { get; set; } = new();
    public BankTransferMapping? BTMappingPayout { get; set; } = new();
    
    public decimal AllowancePL { get; set; } = 0;
    public decimal AllowanceNotPL { get; set; } = 0;
    public decimal? SumAllowancePL { get; set; } = 0;
    public decimal? SumAllowanceNotPL { get; set; } = 0;
    public decimal? DeductionMealsPL
    {
        get
        {
            var sum = DailyMeals
                .Where(meal => meal.CountryCode == "PL")
                .Sum(meal => meal.Total);
            return sum;
        }
    }
    public decimal? DeductionMealsNotPL
    {
        get
        {
            var sum = DailyMeals
                .Where(meal => meal.CountryCode != "PL")
                .Sum(meal => meal.Total);
            

            return sum;
        }
    }
    public decimal? AccomodationAllowanceSumPL { get; set; } = 0;
    public decimal? AccomodationAllowanceSumNotPL { get; set; } = 0;
    public decimal? SumLocalTravelAllowancePL { get; set; } = 0;
    public decimal? SumLocalTravelAllowanceNotPL { get; set; } = 0;
    public decimal? SumPrivateVehicleAllowance
    {
        get
        {
            if(MileageRegister.MileageAllowanceRate != 0)
            {
                return MileageRegister.MileageAllowanceRate * MileageRegister.Entries.Sum(e => e.Mileage);
            } else
            {
                return 0;
            }

           
        }
    }
    public LocationVm CashPoint { get; set; } = new();
    public LocationVm CashPointReceipt { get; set; } = new();
    public decimal TotalBillsPL
    {
        get
        {
            return Bills.Where(bill => bill.Currency == "PLN").Sum(bill => bill.Amount);
        }
    }
    public decimal TotalBillsNotPL
    {
        get
        {
            return Bills.Where(bill => bill.Currency != "PLN").Sum(bill => bill.Amount);
        }
    }
    public decimal TotalAllowancePL
    {
        
        get
        {
            decimal allowanceAfterDeduction = Math.Max(0, SumAllowancePL.GetValueOrDefault() + DeductionMealsPL.GetValueOrDefault());

            return allowanceAfterDeduction
                 + AccomodationAllowanceSumPL.GetValueOrDefault()
                 + SumLocalTravelAllowancePL.GetValueOrDefault()
                 + SumPrivateVehicleAllowance.GetValueOrDefault()
                 + TotalBillsPL
                 - AdvancePaymentAmount.GetValueOrDefault()
                 ;
        }
    }
    public decimal TotalAllowanceNotPL
    {
        get
        {
            return SumAllowanceNotPL.GetValueOrDefault()
                 + AccomodationAllowanceSumNotPL.GetValueOrDefault()
                 + SumLocalTravelAllowanceNotPL.GetValueOrDefault()
                 + DeductionMealsNotPL.GetValueOrDefault()
                 + Transit.Total
                 + TotalBillsNotPL;
        }
    }
    public decimal TotalPayOut
    {
        get
        {
            var totaltotal = TotalAllowanceNotPL * CurrencyExchangeRate + TotalAllowancePL;
            TotalPayOutString = totaltotal.ToString("0.00");
            return totaltotal;
        }
    }
    public string TotalPayOutString { get; set; } = string.Empty;
    public bool SaveOnly { get; set; } = false;
    public int FormVersion { get; set; } = 1;
    public string CurrentApproverName { get; set; } = string.Empty;



    public void Mapping(Profile profile)
    {
        profile.CreateMap<BusinessTravelForm, BusinessTravelFormVm>()
            .ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<FormFileVm>>(src.FormFiles)))
            //.ForMember(dest => dest.BusinessTravelStatuses, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<string>>(src.BusinessTravelStatuses)))
            .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<CountryVm>>(src.Countries)))
            .ForMember(dest => dest.DestinationCountry, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<CountryVm>(src.DestinationCountry)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<ApprovalVm>>(src.Approvals)))
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level2Approvers)))
            .ForMember(dest => dest.Level3Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level3Approvers)))
            .ForMember(dest => dest.Level4Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level4Approvers)))
            .ForMember(dest => dest.Level5Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level5Approvers)))
            .ForMember(dest => dest.Level6Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level6Approvers)))
            .ForMember(dest => dest.FormCostCenter, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<CostCenterVm>(src.FormCostCenter)))
            .ForMember(dest => dest.FormCostLocation, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<LocationVm>(src.FormCostLocation)))
            .ForMember(dest => dest.PayoutCashier, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<EmployeeVm>(src.PayoutCashierEmpId)))
            .ForMember(dest => dest.ReceiptCashier, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<EmployeeVm>(src.ReceiptCashierEmpId)))
            //.ForMember(dest => dest.Files, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<FormFileVm>>(src.Files)))
            //.ForMember(dest => dest.ConveyanceTypes, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<string>>(src.ConveyanceTypes)))
            .ForMember(dest => dest.Stages, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<Stage>>(src.Stages)))
            .ForMember(dest => dest.Accommodations, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<Accommodation>>(src.Accommodations)))
            .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<Meals>>(src.Meals)))
            .ForMember(dest => dest.DailyMeals, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<DailyMeal>>(src.DailyMeals)))
            .ForMember(dest => dest.LocalTravels, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<LocalTravel>>(src.LocalTravels)))
            .ForMember(dest => dest.Transit, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<Transit>(src.Transit)))
            .ForMember(dest => dest.Bills, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<Bill>>(src.Bills)))
            .ForMember(dest => dest.BTMappingAdvancePayment, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<BankTransferMapping>(src.BTMappingAdvancePayment)))
            .ForMember(dest => dest.BTMappingPayout, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<BankTransferMapping>(src.BTMappingPayout)))
            .ForMember(dest => dest.CashPoint, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<LocationVm>(src.CashPoint)))
            .ForMember(dest => dest.RejectReasons, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<RejectReason>>(src.RejectReasons)))
            .ForMember(dest => dest.FormCostCenters, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<SapCostCenterVm>>(src.FormCostCenters)))
            .ForMember(dest => dest.CashPointReceipt, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<LocationVm>(src.CashPointReceipt)))
            .ForMember(dest => dest.MileageRegister, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<MileageRegister>(src.MileageRegister)));

        profile.CreateMap<BusinessTravelFormVm, BusinessTravelForm>()

            .ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormFiles)))
            //.ForMember(dest => dest.BusinessTravelStatuses, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.BusinessTravelStatuses)))
            .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Countries)))
            .ForMember(dest => dest.DestinationCountry, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.DestinationCountry)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Approvals)))
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level2Approvers)))
            .ForMember(dest => dest.Level3Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level3Approvers)))
            .ForMember(dest => dest.Level4Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level4Approvers)))
            .ForMember(dest => dest.Level5Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level5Approvers)))
            .ForMember(dest => dest.Level6Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level6Approvers)))
            .ForMember(dest => dest.FormCostCenter, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormCostCenter)))
            .ForMember(dest => dest.FormCostLocation, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormCostLocation)))
            .ForMember(dest => dest.PayoutCashierEmpId, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.PayoutCashier)))
            .ForMember(dest => dest.ReceiptCashierEmpId, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.ReceiptCashier)))
            //.ForMember(dest => dest.Files, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Files)))
            //.ForMember(dest => dest.ConveyanceTypes, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.ConveyanceTypes)))
            .ForMember(dest => dest.Stages, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Stages)))
            .ForMember(dest => dest.Accommodations, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Accommodations)))
            .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Meals)))
            .ForMember(dest => dest.DailyMeals, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.DailyMeals)))
            .ForMember(dest => dest.LocalTravels, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.LocalTravels)))
            .ForMember(dest => dest.Transit, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Transit)))
            .ForMember(dest => dest.Bills, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Bills)))
            .ForMember(dest => dest.BTMappingAdvancePayment, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.BTMappingAdvancePayment)))
            .ForMember(dest => dest.BTMappingPayout, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.BTMappingPayout)))
            .ForMember(dest => dest.CashPoint, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.CashPoint)))
            .ForMember(dest => dest.CashPointReceipt, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.CashPointReceipt)))
            .ForMember(dest => dest.MileageRegister, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.MileageRegister)))
            .ForMember(dest => dest.RejectReasons, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.RejectReasons)))
            .ForMember(dest => dest.FormCostCenters, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormCostCenters)));
    }

}

