using Application.AdHocJobs;
using Application.Interfaces;
using Application.Mappings;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Forms.Accounting;
using System.Text.Json.Serialization;
using Application.Forms.Accounting.BuisnessTravelSmallClasses;

namespace Application.Forms.Accounting
{
    public class AdvancePaymentFormVm : IMapFrom<AdvancePaymentForm>, IFormAccounting
    {
        #region FromTemplate
        // Properties from FormTemplate
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Zaliczka";
        public string Description { get; set; } = "Formularz do wnioskowania o zaliczkę";
        public string FolderName { get; set; } = "AdvancePayment";
        public List<FormFileVm> FormFiles { get; set; } = new();
        public string NumberPrefix { get; set; } = "ZAL";
        public string Status { get; set; } = "Rejestracja";
        public string? Number { get; set; } = "brak numeru";
        //public List<string> Statuses { get; set; } = new();
        public int WorkflowTemplateId { get; set; }
        #endregion

        public string Objective { get; set; } = string.Empty;
        public decimal? AdvancePaymentAmount { get; set; } = 0;
        public string? AdvancePaymentCurrency { get; set; } = "PLN";
        public bool AdvancePaymentCash { get; set; } = false;
        public string BankAccountNumber { get; set; } = string.Empty;

        public DateOnly? AdvancePaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? CashPayoutNumber { get; set; } = string.Empty;
        public string? PayoutCashierEmpId { get; set; } = string.Empty;
        [JsonIgnore] public EmployeeVm? PayoutCashier { get; set; } = new();

        #region ApproversDetails
        public string? OrganisationSapNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string EnovaEmpId { get; set; } = string.Empty;
        public CostCenterVm FormCostCenter { get; set; } = new CostCenterVm();
        public Application.ViewModels.General.LocationVm FormCostLocation { get; set; } = new();
        public List<ApprovalVm>? Approvals { get; set; } = new();
        public List<RejectReason> RejectReasons { get; set; } = new();

        public List<OrganisationRoleForFormVm> Level1Approvers { get; set; } = new(); // przełożony użytkownika
        public List<OrganisationRoleForFormVm> Level2Approvers { get; set; } = new(); // przełożony przełożonego
        public List<OrganisationRoleForFormVm> Level3Approvers { get; set; } = new(); // Księgowość
        public List<OrganisationRoleForFormVm> Level4Approvers { get; set; } = new(); // Księgowość TeamLeader
        public List<OrganisationRoleForFormVm> Level5Approvers { get; set; } = new(); // kasa

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

        public string? RejectReason { get; set; } = string.Empty;
        #endregion

        public BankTransferMapping? BTMappingAdvancePayment { get; set; } = new();
        public BankTransferMapping? BTMappingPayout { get; set; } = new();

        public Application.ViewModels.General.LocationVm CashPoint { get; set; } = new();

        public bool SaveOnly { get; set; } = false;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AdvancePaymentForm, AdvancePaymentFormVm>()
            .ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<FormFileVm>>(src.FormFiles)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<ApprovalVm>>(src.Approvals)))
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level2Approvers)))
            .ForMember(dest => dest.Level3Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level3Approvers)))
            .ForMember(dest => dest.Level4Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level4Approvers)))
            .ForMember(dest => dest.Level5Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level5Approvers)))
            .ForMember(dest => dest.FormCostCenter, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<CostCenterVm>(src.FormCostCenter)))
            .ForMember(dest => dest.FormCostLocation, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<LocationVm>(src.FormCostLocation)))
            .ForMember(dest => dest.PayoutCashier, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<EmployeeVm>(src.PayoutCashierEmpId)))
            .ForMember(dest => dest.BTMappingAdvancePayment, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<BankTransferMapping>(src.BTMappingAdvancePayment)))
            .ForMember(dest => dest.BTMappingPayout, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<BankTransferMapping>(src.BTMappingPayout)))
            .ForMember(dest => dest.CashPoint, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<LocationVm>(src.CashPoint)))
            .ForMember(dest => dest.RejectReasons, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<RejectReason>>(src.RejectReasons)));

            profile.CreateMap<AdvancePaymentFormVm, AdvancePaymentForm>()
            .ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormFiles)))
            .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Approvals)))
            .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level1Approvers)))
            .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level2Approvers)))
            .ForMember(dest => dest.Level3Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level3Approvers)))
            .ForMember(dest => dest.Level4Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level4Approvers)))
            .ForMember(dest => dest.Level5Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level5Approvers)))
            .ForMember(dest => dest.FormCostCenter, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormCostCenter)))
            .ForMember(dest => dest.FormCostLocation, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormCostLocation)))
            .ForMember(dest => dest.PayoutCashierEmpId, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.PayoutCashierEmpId)))
            .ForMember(dest => dest.BTMappingAdvancePayment, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.BTMappingAdvancePayment)))
            .ForMember(dest => dest.BTMappingPayout, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.BTMappingPayout)))
            .ForMember(dest => dest.CashPoint, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.CashPoint)))
            .ForMember(dest => dest.RejectReasons, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.RejectReasons)));
        }
    }
}