using Application.ViewModels;
using Application.ViewModels.Settlement;
using MediatR;

namespace Application.CQRS.SettlementCQRS.GLAccounts.Queries
{
    public class GetAllGLAccountQuery : IRequest<IQueryable<GLAccountVm>>
    {
    }
}
