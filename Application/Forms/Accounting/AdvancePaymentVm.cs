using Application.ViewModels.Accounting;
using Application.ViewModels.General;
using Microsoft.Kiota.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting
{
    public class AdvancePaymentVm
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

        public string EmployeeName { get; set; } = string.Empty;
        public string EnovaEmpId { get; set; } = string.Empty;
        public string Branch { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
        public bool AdvancePaymentCash { get; set; } = false;
        public string ApprovalL1 { get; set; }
        public string ApprovalL2 { get; set; }
        //public string RecipientName { get; set; }
        public string AccountNumber { get; set; }

        #region ApproversDetails
        public string? OrganisationSapNumber { get; set; } = string.Empty;
        public List<ApprovalVm>? Approvals { get; set; } = new();
        public List<OrganisationRoleForFormVm> Level1Approvers { get; set; } = new(); // przełożony etapy: AprobataL1, AprobataL11
        public List<OrganisationRoleForFormVm> Level2Approvers { get; set; } = new(); // przełożony etapy: AprobataL1, AprobataL11


        public string LVL1_EnovaEmpId { get; set; } = string.Empty;
        public string LVL1_EmployeeName { get; set; } = string.Empty; // manager of user
        public string LVL2_EnovaEmpId { get; set; } = string.Empty;
        public string LVL2_EmployeeName { get; set; } = string.Empty; // manager of user
        #endregion

        public BankTransferMapping? BTMappingAdvancePayment { get; set; } = new();
        public BankTransferMapping? BTMappingPayout { get; set; } = new();
    }
}
