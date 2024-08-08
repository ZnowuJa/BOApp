using Application.Forms;
using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetTestFormByCustomerQuery : IRequest<IQueryable<TestFormVm>>
{
    public string Id { get; set; }
    public GetTestFormByCustomerQuery(string i)
    {
        Id = i;
    }


}
