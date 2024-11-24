using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class UpdateInvoiceItemCommandHandler : IRequestHandler<UpdateInvoiceItemCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdateInvoiceItemCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.InvoiceItems.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        result.Id = request.Id;
        result.Name = request.Name;
        result.PartId = request.PartId;
        result.Qty = request.Qty;
        result.UnitId = request.UnitId;
        result.UnitNetPrice = request.UnitNetPrice;
        result.CurrencyId = request.CurrencyId;
        result.InvoiceId = request.InvoiceId;
        result.ItemsGenerated = request.ItemsGenerated;
        result.Leasing = request.Leasing;
        result.EndOfContract = request.EndOfContract;

        _appDbContext.InvoiceItems.Update(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}
