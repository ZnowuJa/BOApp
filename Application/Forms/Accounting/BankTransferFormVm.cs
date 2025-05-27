using Application.AdHocJobs;
using Application.Forms.Accounting.BankTransferSmallClasses;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;

using AutoMapper;

using Domain.Forms.Accounting;

using RejectReason = Application.Forms.Accounting.BuisnessTravelSmallClasses.RejectReason;
namespace Application.Forms.Accounting;

public class BankTransferFormVm : IFormAccounting
{
    #region FromAuditableEntity
        public int Id { get; set; } = 0;
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
        public int? StatusId { get; set; }
        public string? InactivatedBy { get; set; }
        public DateTime? Inactivated { get; set; }
    #endregion
    #region Initial
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; }
        public string OperationArea { get; private set; } = "Accounting";
        public string Status { get; set; } = "Rejestracja";
        public string Number { get; set; } = string.Empty;
        public string EnovaEmpId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string LVL1_EnovaEmpId { get; set; } //ManagerL1
        public string LVL1_EmployeeName { get; set; } //ManagerL1
        public string LVL1_EmpoyeeEmail { get; set; }
        public string LVL2_EnovaEmpId { get; set; } //ManagerL2
        public string LVL2_EmployeeName { get; set; } //ManagerL2
        public string LVL3_EnovaEmpId { get; set; } //ManagerL3
        public string LVL3_EmployeeName { get; set; } //ManagerL3
        public string LVL4_EnovaEmpId { get; set; } //Accountant
        public string LVL4_EmployeeName { get; set; } //Accountant 
        public string LVL5_EnovaEmpId { get; set; } //Accountant Team Leader
        public string LVL5_EmployeeName { get; set; } //Accountant Team Leader

        public List<OrganisationRoleForFormVm> Level1Approvers { get; set; } = new();
        public List<OrganisationRoleForFormVm> Level2Approvers { get; set; } = new();
        public List<OrganisationRoleForFormVm> Level3Approvers { get; set; } = new();
        public List<OrganisationRoleForFormVm> Level4Approvers { get; set; } = new();
        public List<OrganisationRoleForFormVm> Level5Approvers { get; set; } = new();
        public string CurrentApproverName { get; set; }
        public List<FormFileVm> FormFiles { get; set; }
        public string NumberPrefix { get; set; }
        public string FolderName { get; set; }
        
        public string? OrganisationSapNumber { get; set; } = string.Empty;
        public SapCostCenterVm EmployeeSapCostCenterVm { get; set; } = new SapCostCenterVm();
        
        public List<SapCostCenterVm> FormCostCenters = new();
        public List<ApprovalVm>? Approvals { get; set; } = new();
        public List<RejectReason> RejectReasons { get; set; } = new();
        public string? RejectReason { get; set; } = string.Empty;
    #endregion

    #region Recipient

    public string RecipientName { get; set; }
    public string RecipientAddressStreet { get; set; }
    public string RecipientAddressCity { get; set; }
    public string RecipientAddressPostCode { get; set; }
    public string RecipientAddressCountry { get; set; }
    public string RecipientVatId { get; set; }
    public bool IsIndividual { get; set; } = false;

    #endregion
    #region Document
        public Invoice Document { get; set; }
        
        public decimal Amount { get; set; } = 0;

        public CurrencyVm Currency { get; set; } = new CurrencyVm();
        //public string BankTransferTitle { get; set; }
        public bool SplitPayment { get; set; }
        public VATRateVm SplitPaymentVatRate { get; set; }
        public decimal SplitPaymentAmount { get; set; }
        
        public string Notes { get; set; }
        
        public List<InvoiceMapping> InvoiceMappings { get; set; }
        public BankTransferMapping BankTransferMapping { get; set; } = new BankTransferMapping();
        public string Duties { get; set; } = string.Empty;
        public string PccTaxes { get; set; } = string.Empty;

    #endregion
    #region Steering
    public bool SaveOnly { get; set; } = false;


    #endregion
        public int FormVersion { get; set; } = 1;
        public string FormType { get; set; } = string.Empty;
        public List<Attachment> Attachments = new();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BankTransferForm, BankTransferFormVm>()
                .ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<FormFileVm>>(src.FormFiles)))
                .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level1Approvers)))
                .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level2Approvers)))
                .ForMember(dest => dest.Level3Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level3Approvers)))
                .ForMember(dest => dest.Level4Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level4Approvers)))
                .ForMember(dest => dest.Level5Approvers, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<OrganisationRoleForFormVm>>(src.Level5Approvers)))
                .ForMember(dest => dest.EmployeeSapCostCenterVm, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<SapCostCenterVm>(src.EmployeeSapCostCenterVm)))
                .ForMember(dest => dest.FormCostCenters, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<SapCostCenterVm>>(src.FormCostCenters)))
                .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<ApprovalVm>>(src.Approvals)))
                .ForMember(dest => dest.RejectReasons, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<RejectReason>>(src.RejectReasons)))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<Invoice>(src.Document)))
                .ForMember(dest => dest.InvoiceMappings, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<List<InvoiceMapping>>(src.InvoiceMappings)))
                .ForMember(dest => dest.BankTransferMapping, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<BankTransferMapping>(src.BankTransferMapping)))
                .ForMember(dest => dest.Duties, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<Duty>(src.Duties)))
                .ForMember(dest => dest.PccTaxes, opt => opt.MapFrom(src => AppUtils.SafeDeserialize<Duty>(src.PccTaxes)));
        

            profile.CreateMap<BankTransferFormVm, BankTransferForm>()
                .ForMember(dest => dest.FormFiles, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormFiles)))
                .ForMember(dest => dest.Level1Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level1Approvers)))
                .ForMember(dest => dest.Level2Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level2Approvers)))
                .ForMember(dest => dest.Level3Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level3Approvers)))
                .ForMember(dest => dest.Level4Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level4Approvers)))
                .ForMember(dest => dest.Level5Approvers, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Level5Approvers)))
                .ForMember(dest => dest.EmployeeSapCostCenterVm, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.EmployeeSapCostCenterVm)))
                .ForMember(dest => dest.FormCostCenters, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.FormCostCenters)))
                .ForMember(dest => dest.Approvals, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Approvals)))
                .ForMember(dest => dest.RejectReasons, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.RejectReasons)))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Document)))
                .ForMember(dest => dest.InvoiceMappings, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.InvoiceMappings)))
                .ForMember(dest => dest.BankTransferMapping, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.BankTransferMapping)))
                .ForMember(dest => dest.Duties, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.Duties)))
                .ForMember(dest => dest.PccTaxes, opt => opt.MapFrom(src => AppUtils.SafeSerialize(src.PccTaxes)));
            }


    
}

public class Invoice
{
    public Guid Id { get; set; }
    public string DocumentType { get; set; } = "Invoice";
    public decimal Amount { get; set; } = 0m;
    public string InvoiceNumber { get; set; }
    public DateOnly DocumentDate { get; set; }
    public DateOnly IssueDate { get; set; }
    public string IssuerName { get; set; }
    public string IssuerVatId { get; set; }
    public string Description { get; set; }
    public string IssuerAddressStreet { get; set; }
    public string IssuerAddressCity { get; set; }
    public string IssuerAddressPostalCode { get; set; }
    public CountryVm Country { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankSwiftNumber { get; set; }
    public CurrencyVm Currency { get; set; }
    
}
public class Receipt
{
    public Guid Id { get; set; }
    public string DocumentType { get; set; } = "Invoice";
}
