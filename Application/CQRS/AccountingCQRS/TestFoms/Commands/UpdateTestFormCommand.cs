using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
