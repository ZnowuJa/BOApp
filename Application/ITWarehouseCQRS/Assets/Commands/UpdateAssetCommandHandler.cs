using System.Security.Cryptography;

using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Assets.Commands;
public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdateAssetCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Assets.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        item.PartId = request.PartId;
        item.InvoiceId = request.InvoiceId;
        item.InvoiceItemId = request.InvoiceItemId;
        item.StateId = request.StateId;
        item.AssetTagNumber = request.AssetTagNumber;
        item.SerialNumber = request.SerialNumber;
        item.LastSeen = request.LastSeen;
        item.AssigneeId = request.AssigneeVmId;
        item.AssigneeName = request.AssigneeVmName;
        item.AssigneeType = request.AssigneeVmType;
        item.WarehouseId = request.WarehouseId;
        item.Price = request.Price;
        item.CurrencyId = request.CurrencyId;
        item.PurchaseDate = request.PurchaseDate;
        item.Leasing = request.Leasing;
        item.EndOfContract = request.EndOfContract;
        item.WarrantyUntil = request.WarrantyUntil;
        item.Imei = request.Imei;
        item.Mac = request.Mac;


        _appDbContext.Assets.Update(item);

        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
