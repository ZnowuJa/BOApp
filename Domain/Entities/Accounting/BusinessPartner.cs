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
        public string Name { get; set; } = string.Empty;
        public string LongName { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string VatId { get; set; } = string.Empty;
        public string SAPId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}