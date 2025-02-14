using Application.Interfaces;
using Application.Mappings;
using Application.ViewModels.Accounting;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Entities.Accounting;
using Domain.Forms.Accounting;
using Microsoft.Kiota.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        public Application.ViewModels.General.Location FormCostLocation { get; set; } = new();
        public List<ApprovalVm>? Approvals { get; set; } = new();

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

        public Application.ViewModels.General.Location CashPoint { get; set; } = new();
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AdvancePaymentForm, AdvancePaymentFormVm>().ReverseMap();
        }
    }
}
