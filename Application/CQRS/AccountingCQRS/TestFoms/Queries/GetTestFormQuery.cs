using Application.Forms;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.TestForms.Queries;
public class GetTestFormQuery : IRequest<TestFormVm>
{
    public GetTestFormQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}
