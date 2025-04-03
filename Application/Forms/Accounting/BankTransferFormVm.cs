using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;
using Microsoft.Graph.Models;
using RejectReason = Application.Forms.Accounting.BuisnessTravelSmallClasses.RejectReason;

namespace Application.Forms.Accounting;

public class BankTransferFormVm : IFormAccounting
{
    #region Initial
        public int Id { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string OperationArea { get; private set; } = "Accounting";
        public string Status { get; set; }
        public string Number { get; set; }
        public string EnovaEmpId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string LVL1_EnovaEmpId { get; set; }
        public string LVL1_EmployeeName { get; set; }
        public string LVL2_EnovaEmpId { get; set; }
        public string LVL2_EmployeeName { get; set; }
        public string LVL3_EnovaEmpId { get; set; }
        public string LVL3_EmployeeName { get; set; }
        public string LVL4_EnovaEmpId { get; set; }
        public string LVL4_EmployeeName { get; set; }
        public string LVL5_EnovaEmpId { get; set; }
        public string LVL5_EmployeeName { get; set; }

        public List<OrganisationRoleForFormVm> Level1Approvers { get; set; }
        public List<OrganisationRoleForFormVm> Level2Approvers { get; set; }
        public List<OrganisationRoleForFormVm> Level3Approvers { get; set; }
        public List<OrganisationRoleForFormVm> Level4Approvers { get; set; }
        public List<OrganisationRoleForFormVm> Level5Approvers { get; set; }
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

    #region Document
        public Invoice Document { get; set; }
        public string BankTransferTitle { get; set; }
        public bool SplitPayment { get; set; }
        
        public string Notes { get; set; }
        
        public List<InvoiceMapping> InvoiceMappings { get; set; }
        public BankTransferMapping BankTransferMapping { get; set; }
    
    #endregion

    
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
