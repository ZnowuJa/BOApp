using AutoMapper;
using BackOfficeApp_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Accounting
{
    public class BusinessPartner : SmallAuditableEntity
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? VatId { get; set; }
        public string? SAPId { get; set; }
        public string BankTransferType { get; set; }
        public string BusinessPartnerType { get; set; }
        public string? Location { get; set; }
        public string SAPFormType { get; set; }
        public string DefaultCurrency {  get; set; }
    }
}