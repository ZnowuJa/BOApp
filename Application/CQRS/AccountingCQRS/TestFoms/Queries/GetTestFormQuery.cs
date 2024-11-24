using Application.Forms;
using MediatR;

namespace Application.CQRS.AccountingCQRS.TestForms.Queries;
public class GetTestFormQuery : IRequest<TestFormVm>
{
    public GetTestFormQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}
