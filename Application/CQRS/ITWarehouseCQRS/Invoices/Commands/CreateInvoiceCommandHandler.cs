using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;


namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateInvoiceCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
       
        Invoice item = new()
        {
            Number = request.Number,
            CompanyId = request.CompanyVmId,
            Date = request.Date,
            TotalNet = request.TotalNet,
            CurrencyId = request.CurrencyVmId,

        };

        _appDbContext.Invoices.Add(item);
        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
