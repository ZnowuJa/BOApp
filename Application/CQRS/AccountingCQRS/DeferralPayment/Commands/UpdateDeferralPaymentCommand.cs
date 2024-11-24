using Application.Forms.Accounting;
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
