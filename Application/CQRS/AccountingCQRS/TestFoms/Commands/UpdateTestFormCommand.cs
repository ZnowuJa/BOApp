using Application.Forms;
using MediatR;

namespace Application.CQRS.AccountingCQRS.TestForms.Commands;
public class UpdateTestFormCommand : IRequest<TestFormVm>
{
    public TestFormVm Item { get; set; }

    public UpdateTestFormCommand(TestFormVm item)
    {
        Item = item;
    }

}
