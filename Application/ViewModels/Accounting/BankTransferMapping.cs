using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.General;

namespace Application.ViewModels.Accounting
{
    public class BankTransferMapping
    {
        public DateTime? PostingDate { get; set; } = DateTime.Now;
        public DateTime? CurrencyExchangeDate { get; set; } = DateTime.Now;
        public string SapFormType { get; set; } = string.Empty;
        public bool InSAP { get; set; } = false;
        public bool TLChecked { get; set; } = false;
        public string Comment { get; set; } = string.Empty;
        public string SapDocNumber { get; set; } = string.Empty;
        public string SapPaymentKey { get; set; } = string.Empty;
        public string BankTrasferTitle { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public int AccountantEmpId { get; set; } = 0;
        public string AccountantName { get; set; } = string.Empty;
        public string AccountantEmail { get; set; } = string.Empty;
        public int AccountantTLEmpId { get; set; } = 0;
        public string AccountantTLName { get; set; } = string.Empty;
        public string AccountantTLEmail { get; set; } = string.Empty;
        public DateTime? AccountantTime { get; set; } 
        public DateTime? AccountantTLTime { get; set; } 
        //public CostCenterVm CostCenter { get; set; } = new();
        public SapCostCenterVm? sapCostCenter { get; set; } = new();
        public GLAccountVm GLAccount { get; set; } = new();
        public bool BusinessTravel { get; set; } = false; //wyłącza pola jeśli true (np. datawaluta)
    }


}


