using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.Customer.Queries;
public class GetCustomerQuery(int i) : IRequest<CustomerVm>
{
    public int Id { get; set; } = i;
}
public class GetCustomerQueryHandler(IAsDbContext asDbContext, IMapper mapper) : IRequestHandler<GetCustomerQuery, CustomerVm>
{
    private readonly IAsDbContext _asDbContext = asDbContext;
    private readonly IMapper _mapper = mapper;

    public IMapper Mapper { get; }

    public async Task<CustomerVm> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        //var temp = new DeferralPaymentFormVm();
        var item = await _asDbContext.v_KONTRAHENCI_LISTA.Where(p => p.KontrahentId == request.Id).FirstOrDefaultAsync(cancellationToken);
        var itemVM = _mapper.Map<CustomerVm>(item);
        return itemVM;
        //return temp;
    }
}