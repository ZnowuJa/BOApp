using Application.Forms.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentByCustomerQuery : IRequest<IQueryable<DeferralPaymentFormVm>>
{
    public string Id { get; set; }
    public GetDeferralPaymentByCustomerQuery(string i)
    {
        Id = i;
    }


}
