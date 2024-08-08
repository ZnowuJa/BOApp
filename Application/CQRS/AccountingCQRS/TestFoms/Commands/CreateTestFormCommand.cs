using Application.Forms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.TestForms.Commands;
public class CreateTestFormCommand : IRequest<TestFormVm>
{
    public TestFormVm Item { get; set; }

    public CreateTestFormCommand(TestFormVm item)
    {
        Item = item;
    }
}
