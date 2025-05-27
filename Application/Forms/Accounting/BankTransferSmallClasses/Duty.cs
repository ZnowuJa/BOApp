using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forms.Accounting.BankTransferSmallClasses;
public class Duty
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly ReceiveDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly DocumentDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public string ReferenceNumber { get; set; } = string.Empty;
    public decimal NetValue {  get; set; } = 0m;
    public decimal VatValue { get; set; } = 0m;
    public decimal DutyValue { get; set; } = 0m;

}
