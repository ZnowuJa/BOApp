using Application.Forms.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Commands;
public class CreateDeferralPaymentCommand : IRequest<DeferralPaymentFormVm>
{
    public DeferralPaymentFormVm Item { get; set; }

    public CreateDeferralPaymentCommand(DeferralPaymentFormVm item)
    {
        Item = item;
    }
}
