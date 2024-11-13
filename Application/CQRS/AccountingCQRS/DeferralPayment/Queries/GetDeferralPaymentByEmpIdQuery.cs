using Application.Forms.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentByEmpIdQuery : IRequest<IQueryable<DeferralPaymentFormVm>>
{
    public string Id { get; set; }
    public GetDeferralPaymentByEmpIdQuery(string i)
    {
        Id = i;
    }


}
