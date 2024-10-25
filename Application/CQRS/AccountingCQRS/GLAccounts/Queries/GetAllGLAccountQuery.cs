using Application.ViewModels;
using Application.ViewModels.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Queries
{
    public class GetAllGLAccountQuery : IRequest<IQueryable<GLAccountVm>>
    {
    }
}
