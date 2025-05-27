using Domain.Common;

namespace Domain.Forms.Accounting;

public class BankTransferForm : FormTemplate
{
    public BankTransferForm() : base("Polecenie przelewu", "Formularz do poleceń przelewów", "BankTransfers", "PZ", "Accounting", "Rejestracja", 6)
    {
        Statuses = GetDefaultStatuses();
    }
    
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

        public string Level1Approvers { get; set; }
        public string Level2Approvers { get; set; }
        public string Level3Approvers { get; set; }
        public string Level4Approvers { get; set; }
        public string Level5Approvers { get; set; }
        public string CurrentApproverName { get; set; }
        public string FormFiles { get; set; }
        
        public string OrganisationSapNumber { get; set; } = string.Empty;
        public string EmployeeSapCostCenterVm { get; set; } = string.Empty;
        
        public string FormCostCenters = string.Empty;
        public string Approvals { get; set; } = string.Empty;
        public string RejectReasons { get; set; } = string.Empty;
        public string RejectReason { get; set; } = string.Empty;
    

    
        public string Document { get; set; }
        public string BankTransferTitle { get; set; }
        public bool SplitPayment { get; set; }
        
        public string Notes { get; set; }
        
        public string InvoiceMappings { get; set; }
        public string BankTransferMapping { get; set; }
        public int FormVersion { get; set; } = 1;
        public string FormType { get; set; } = string.Empty;
        public string Duties { get; set; } = string.Empty;
        public string PccTaxes { get; set; } = string.Empty;




    public static List<string> GetDefaultStatuses()
    {
        return new List<string>
        {
            "Rejestracja", "AprobataL1", "AprobataL2", "AprobataL3", "Ksiegowosc", "KsiegowoscTL",  "Kasa", "WyslaneDoRobota", "BladRobota", "Rozliczone", "Zamkniete", "Odrzucone"
        };
    }
}