using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forms.Accounting
{
    public class AdvancePaymentForm : FormTemplate
    {
        public AdvancePaymentForm() : base("Zaliczka", "Formularz do wnioskowania o zaliczkę", "AdvancePayment", "ZAL", "Accounting", "Rejestracja", 5)
        {
            Statuses = GetDefaultStatuses();
        }

        public string FormFiles { get; set; } = string.Empty;

        #region Approvers&Approvals
        public string? OrganisationSapNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string EnovaEmpId { get; set; } = string.Empty;
        public string FormCostCenter { get; set; } = string.Empty;
        public string FormCostLocation { get; set; } = string.Empty;

        public string? Approvals { get; set; } = string.Empty;
        public string Level1Approvers { get; set; } = string.Empty;
        public string Level2Approvers { get; set; } = string.Empty;
        public string Level3Approvers { get; set; } = string.Empty;
        public string Level4Approvers { get; set; } = string.Empty;
        public string Level5Approvers { get; set; } = string.Empty;

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

        public string RejectReason { get; set; } = string.Empty;
        public string RejectReasons { get; set; } = string.Empty;
        #endregion

        public string Objective { get; set; } = string.Empty;
        public decimal? AdvancePaymentAmount { get; set; } = 0;
        public decimal? AdvancePaymentBalance { get; set; }
        public string? AdvancePaymentCurrency { get; set; } = "PLN";
        public bool AdvancePaymentCash { get; set; } = false;
        public string? BankAccountNumber { get; set; } = string.Empty;
        public DateOnly? AdvancePaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? CashPayoutNumber { get; set; } = string.Empty;
        public string? PayoutCashierEmpId { get; set; } = string.Empty;

        public string BTMappingAdvancePayment { get; set; } = string.Empty;
        public string BTMappingPayout { get; set; } = string.Empty;

        public string CashPoint { get; set; } = string.Empty;

        public static List<string> GetDefaultStatuses()
        {
            return new List<string>
            {
                "Rejestracja", "AprobataL1", "AprobataL2", "Ksiegowosc", "WyslaneDoRobota", "KsiegowoscTL", "Kasa", "Zamkniete", "Odrzucone"
            };
        }
    }
}
