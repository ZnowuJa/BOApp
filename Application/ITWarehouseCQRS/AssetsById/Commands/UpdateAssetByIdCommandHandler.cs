using Application.Interfaces;
using Application.ITWarehouseCQRS.Assets.Commands;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.AssetsById.Commands;
public class UpdateAssetByIdCommandHandler : IRequestHandler<UpdateAssetByIdCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdateAssetByIdCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateAssetByIdCommand request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Assets.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        item.Part = await _appDbContext.Parts.Where(p => p.Id == request.PartId).FirstOrDefaultAsync();
        item.Invoice = await _appDbContext.Invoices.Where(p => p.Id == request.InvoiceId).FirstOrDefaultAsync();
        item.State = await _appDbContext.States.Where(p => p.Id == request.StateId).FirstOrDefaultAsync();
        item.AssetTagNumber = request.AssetTagNumber;
        item.SerialNumber = request.SerialNumber;
        item.LastSeen = request.LastSeen;
        item.Employee = await _appDbContext.Employees.Where(p => p.Id == request.EmployeeId).FirstOrDefaultAsync();
        item.Warehouse = await _appDbContext.Warehouses.Where(p => p.Id == request.WarehouseId).FirstOrDefaultAsync();
        item.Price = request.Price;
        item.Currency = await _appDbContext.Currencies.Where(p => p.Id == request.CurrencyId).FirstOrDefaultAsync();
        item.PurchaseDate = request.PurchaseDate;



        _appDbContext.Assets.Update(item);

        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
