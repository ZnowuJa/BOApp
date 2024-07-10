using Application.Forms;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Commands;
public class CreateDeferralPaymentCommand : IRequest<DeferralPaymentFormVm>
{
    public DeferralPaymentFormVm Item { get; set; }

    public CreateDeferralPaymentCommand(DeferralPaymentFormVm item)
    {
        Item = item;
    }
}
