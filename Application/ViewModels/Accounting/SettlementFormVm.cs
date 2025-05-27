using Application.ViewModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting
{
    public class SettlementFormVm
    {
        #region FromTemplate 
        // Properties from FormTemplate
        public string Name { get; set; } = "Rozliczenie";
        public string Description { get; set; } = "Formularz do rozliczania.";
        public string FolderName { get; set; } = "SettlementForm";
        public List<FormFileVm> FormFiles { get; set; } = new();
        public string NumberPrefix { get; set; } = "ROZ";
        public string Status { get; set; } = "Rejestracja";
        public string? Number { get; set; } = "brak numeru";
        public int WorkflowTemplateId { get; set; } = 5;
        #endregion

        //public string SettlementNumber { get; set; }
        public string SettlementType { get; set; } = string.Empty;

        public string EmployeeEmail { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string EnovaEmpId { get; set; } = string.Empty;
        public CostCenterVm FormCostCenter { get; set; } = new CostCenterVm();
        public LocationVm FormCostLocation { get; set; } = new();
        public List<SapCostCenterVm> FormCostCenters = new();
        public List<ApprovalVm>? Approvals { get; set; } = new();

        public string OrganisationSapNumber { get; set; } = string.Empty;
        public string InvoiceIssuer { get; set; } = string.Empty; //Wystawca Faktury

        public string DocumentNumber { get; set; } = string.Empty;                     // Numer Dokumentu
        public string TaxIdentificationNumber { get; set; } = string.Empty;           // NIP (z kodem kraju)
        public DateTime? IssueDate { get; set; } = DateTime.Now;                     // Data wystawienia
        public decimal? GrossAmount { get; set; } = 0;                                   // Faktura Brutto
        public string InvoiceTitle { get; set; } = string.Empty;                   // Tytułem

        public bool IsWarehouseRelated { get; set; } = false;                    // Magazyn: - 0/1
        public string PZNumber { get; set; } = string.Empty;                    // Numer PZ
        public decimal? TotalAmount { get; set; } = 0;                         // Razem
        public bool IsSettlementByCash { get; set; } = false;                 // Sposób rozliczenia: - 0/1

        public string ApprovalLevel1 { get; set; }                          // AprobataL1
        public string ApprovalLevel2 { get; set; }                         // AprobataL2
        public string CashDesk { get; set; }                              // Kasa

        public string DeclarationText { get; set; } = string.Empty;                   // Treść oświadczenia
        public decimal? DeclarationAmount { get; set; } = 0;                // Kwota
        public string DeclarationCurrency { get; set; } = "PLN";
        public DateTime? DeclarationDate { get; set; } = DateTime.Now;                // Data
        public string DeclarationTitle { get; set; } = string.Empty; // Tytułem

        public DateOnly? ReceiptPaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? CashReceiptNumber { get; set; } = string.Empty;
        public string ReceiptTitle { get; set; } = string.Empty;               // Tytułem
        public string? ReceiptBankAccountNumber { get; set; } = string.Empty;
        public string? ReceiptCashierEmpId { get; set; } = string.Empty;

        public BankTransferMapping? BTMappingAdvancePayment { get; set; } = new();
        public BankTransferMapping? BTMappingPayout { get; set; } = new();
        public LocationVm CashPoint { get; set; } = new();
        public LocationVm CashPointReceipt { get; set; } = new();

        public string AccountingNote { get; set; }           // Dekretacja do uzupełnienia
    }
}
