using Application.Forms.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentQuery : IRequest<DeferralPaymentFormVm>
{
    public GetDeferralPaymentQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}
