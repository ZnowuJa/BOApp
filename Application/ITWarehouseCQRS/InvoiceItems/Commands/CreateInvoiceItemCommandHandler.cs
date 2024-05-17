using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class CreateInvoiceItemCommandHandler : IRequestHandler<CreateInvoiceItemCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateInvoiceItemCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateInvoiceItemCommand request, CancellationToken cancellationToken)
    {
       
        InvoiceItem item = new()
        {
            Name = request.Name,
            PartId = request.PartId,
            Qty = request.Qty,
            UnitId = request.UnitId,
            UnitNetPrice = request.UnitNetPrice,
            CurrencyId = request.CurrencyId,
            InvoiceId = request.InvoiceId,
            ItemsGenerated = request.ItemsGenerated,

        };

        _appDbContext.InvoiceItems.Add(item);
        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
