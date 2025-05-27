using Application.Interfaces;
using Application.ITWarehouseCQRS.Assets.Commands;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.AssetsById.Commands;
public class CreateAssetByIdCommandHandler : IRequestHandler<CreateAssetByIdCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateAssetByIdCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateAssetByIdCommand request, CancellationToken cancellationToken)
    {
        Asset Asset = new()
        {
            Part = await _appDbContext.Parts.Where(p => p.Id == request.PartId).FirstOrDefaultAsync(),
            Invoice = await _appDbContext.Invoices.Where(p => p.Id == request.InvoiceId).FirstOrDefaultAsync(),
            State = await _appDbContext.States.Where(p => p.Id == request.StateId).FirstOrDefaultAsync(),
            AssetTagNumber = request.AssetTagNumber,
            SerialNumber = request.SerialNumber,
            LastSeen = request.LastSeen,
            Employee = await _appDbContext.Employees.Where(p => p.Id == request.EmployeeId).FirstOrDefaultAsync(),
            Warehouse = await _appDbContext.Warehouses.Where(p => p.Id == request.WarehouseId).FirstOrDefaultAsync(),
            Currency = await _appDbContext.Currencies.Where(p => p.Id == request.CurrencyId).FirstOrDefaultAsync(),
            PurchaseDate = request.PurchaseDate
        };
        _appDbContext.Assets.Add(Asset);
        await _appDbContext.SaveChangesAsync();
        return Asset.Id;
    }
}
