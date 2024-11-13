using Application.Forms.Accounting;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetDeferralPaymentQuery : IRequest<DeferralPaymentFormVm>
{
    public GetDeferralPaymentQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}
