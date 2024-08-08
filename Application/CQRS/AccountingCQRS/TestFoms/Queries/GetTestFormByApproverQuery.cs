using Application.Forms;
using MediatR;

namespace Application.CQRS.AccountingCQRS.TestForms.Queries;
public class GetTestFormByApproverQuery : IRequest<IQueryable<TestFormVm>>
{
    public string Id { get; set; }
    public GetTestFormByApproverQuery(string i)
    {
        Id = i;
    }


}
