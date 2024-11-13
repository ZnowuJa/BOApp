using Application.Forms.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentByApproverQuery : IRequest<IQueryable<DeferralPaymentFormVm>>
{
    public string Id { get; set; }
    public GetDeferralPaymentByApproverQuery(string i)
    {
        Id = i;
    }


}
