using Application.Forms;
using MediatR;

namespace Application.CQRS.AccountingCQRS.TestForms.Commands;
public class CreateTestFormCommand : IRequest<TestFormVm>
{
    public TestFormVm Item { get; set; }

    public CreateTestFormCommand(TestFormVm item)
    {
        Item = item;
    }
}
