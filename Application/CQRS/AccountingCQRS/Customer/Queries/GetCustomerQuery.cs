using Application.ViewModels.Accounting;

using MediatR;

namespace Application.CQRS.AccountingCQRS.Customer.Queries;
public class GetCustomerQuery : IRequest<CustomerVm>
{
    public GetCustomerQuery(int i)
    {
        Id = i;
    }

    public int Id { get; set; }
}
