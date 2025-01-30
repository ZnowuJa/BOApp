using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting;
public class BusinessPartner
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string VatId { get; set; } = string.Empty;
    public string SAPId { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string BankAccountNumber { get; set; } = string.Empty;

}