using Application.Forms.Accounting;
using Application.ViewModels;
using MediatR;

namespace Application.CQRS.AccountingCQRS.DeferralPayment.Queries;
public class GetAllDeferralPaymentFormQuery : IRequest<IQueryable<DeferralPaymentFormVm>>
{
}
