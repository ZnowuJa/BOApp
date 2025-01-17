using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Domain.Common;
using Domain.Entities.Accounting;
using Domain.Entities.Common;

using Domain.Entities.ITWarehouse;
using Domain.Forms.ITForms;

namespace Domain.Forms.Accounting
{
    public class BusinessTravelForm : FormTemplate
    {
        public BusinessTravelForm() : base("Delegacja", "Formularz do rozliczania Delegacji pracowniczych.", "BusinessTravels", "DEL", "Accounting", "Rejestracja", 5)
        {
            Statuses = GetDefaultStatuses();
        }
        
        public int Id { get; set; } = 0;
        public string FormFiles { get; set; } = string.Empty;
        public string? Number { get; set; } = "brak numeru";
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; } = DateTime.Now;
        public string? Destination { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Countries { get; set; } = string.Empty; 
        public string? DestinationCountry { get; set; } = string.Empty;
        public string? DestinationCountryCurrency { get; set; } = string.Empty;
        #region Transport
        public bool PrivateVehicle { get; set; } = false; //does trip requires private car= false;
        public int PrivateVehicleEngineSize { get; set; } = 0;//private
        public int PrivateVehicleMilage { get; set; } = 0;
        public string PrivateVehicleNumber { get; set; } = string.Empty;
        public bool CompanyVehicle { get; set; } = false;
        public string CompanyVehicleNumber { get; set; } = string.Empty;
        public bool PublicTransport { get; set; } = false;
        public bool PublicTransportPaid { get; set; } = false;
        #endregion

        #region Approvers&Approvals
        public string? OrganisationSapNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string EnovaEmpId { get; set; } = string.Empty;
        public string? Approvals { get; set; } = string.Empty;
        public string? Level1Approvers { get; set; } = string.Empty; // przełożony wniosek
        public string Level2Approvers { get; set; } = string.Empty; // przełożony rozliczenie
        public string Level3Approvers { get; set; } = string.Empty; // dyrektor salonu
        public string Level4Approvers { get; set; } = string.Empty; // Księgowość
        public string Level5Approvers { get; set; } = string.Empty; // Księgowość TeamLeader
        public string Level6Approvers { get; set; } = string.Empty; // Księgowość TeamLeader
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
        public string LVL6_EnovaEmpId { get; set; } = string.Empty;
        public string LVL6_EmployeeName { get; set; } = string.Empty;
        public string RejectReason { get; set; } = string.Empty;
        #endregion

        #region AdvancePayment
        public bool AdvancePayment { get; set; } = false;
        public decimal? AdvancePaymentAmount { get; set; } = 0;
        public string? AdvancePaymentCurrency { get; set; } = "PLN";
        public bool? AdvancePaymentCash { get; set; } = false;
        public string? BankAccountNumber { get; set; } = string.Empty;
        // dorobic walidację numeru konta
        public DateOnly? AdvancePaymentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? CashPayoutNumber { get; set; } = string.Empty;
        public string? CashReceiptNumber { get; set; } = string.Empty;
        public string? PayoutCashierEmpId { get; set; } = string.Empty;
        public string? ReceiptCashierEmpId { get; set; } = string.Empty;
        public decimal CurrencyExchamngeRate { get; set; } = 1m;
        public DateOnly CurrencyExchangeRateDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        // do delegacji zagranicznej to min. 25% diety należnej wg kraju przeznaczenia i wg ilości naliczonej diety czyli czasu na jaki pracownik wypełnia delegację
        #endregion
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        //public string Files { get; set; } = string.Empty;
        public string ConveyanceTypes { get; set; } = string.Empty;

        public string Stages { get; set; } = string.Empty;
        public string Accommodations { get; set; } = string.Empty;
        public string Meals { get; set; } = string.Empty;
        public string LocalTravels { get; set; } = string.Empty;
        public string Transit { get; set; } = string.Empty;
        public string Bills { get; set; } = string.Empty;
        public string BTMappingAdvancePayment { get; set; } = string.Empty;
        public string BTMappingPayout { get; set; } = string.Empty;
        public decimal AllowancePL { get; set; } = 0;
        public decimal AllowanceNotPL { get; set; } = 0;
        public decimal? SumAllowancePL { get; set; } = 0;
        public decimal? SumAllowanceNotPL { get; set; } = 0;
        public decimal? DeductionMealsPL { get; set; } = 0;
        public decimal? DeductionMealsNotPL { get; set; } = 0;
        public decimal? AccomodationAllowanceSumPL { get; set; } = 0;
        public decimal? AccomodationAllowanceSumNotPL { get; set; } = 0;
        public decimal? SumLocalTravelAllowancePL { get; set; } = 0;
        public decimal? SumLocalTravelAllowanceNotPL { get; set; } = 0;
        public decimal? SumPrivateVehicleAllowance { get; set; } = 0;
        public string CashPoint { get; set; } = string.Empty;
        public decimal TotalBillsPL { get; set; } = 0;
        public decimal TotalBillsNotPL { get; set; } = 0;
        public decimal TotalAllowancePL { get; set; } = 0;
        public decimal TotalAllowanceNotPL { get; set; } = 0;
        public decimal TotalPayOut { get; set; } = 0;

        public static List<string> GetDefaultStatuses()
        {
            return new List<string>
            {
                "Rejestracja", "AprobataL1", "AprobataL2", "ZaliczkaKasa", "ZaliczkaKsiegowosc", "ZaliczkaKsiegowoscTL", "Rozliczenie", "Ksiegowosc", "KsiegowoscTL", "AprobataL11", "AprobataL12", "KasaRozliczenie", "WyslaneDoRobota", "Rozliczone", "Zamkniete"
            };
        }

    }


}
