using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms;

using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Commands;
public class UpdateDeferralPaymentCommand : IRequest<DeferralPaymentFormVm>
{
    public DeferralPaymentFormVm Item { get; set; }

    public UpdateDeferralPaymentCommand(DeferralPaymentFormVm item)
    {
        Item = item;
    }

}
