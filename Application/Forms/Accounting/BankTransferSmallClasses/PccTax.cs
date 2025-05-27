using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Application.Forms.Accounting.BankTransferSmallClasses;
public class PccTax
{
    public Guid Id { get; set; } = new Guid();
    public string AgreementNumber { get; set; } = string.Empty;
    public string VIN { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0m;
    public string InvoiceNumber { get; set; } = string.Empty;
    public string LocationNumber { get; set; } = string.Empty;
    public string GLAccount { get; set; } = string.Empty;
    public string DepartmentNumber { get; set; } = string.Empty;
}
