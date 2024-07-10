using Application.Forms;
using Application.ViewModels;
using Application.ViewModels.Accounting;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.Customer.Queries;
public class GetCustomerQuery : IRequest<CustomerVm>
{
    public GetCustomerQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}
