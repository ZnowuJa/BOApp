using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Assets.Commands;
public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateAssetCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        Asset Asset = new()
        {
            PartId = request.PartId,
            InvoiceId = request.InvoiceId,
            InvoiceItemId = request.InvoiceItemId,
            StateId = request.StateId,
            AssetTagNumber = request.AssetTagNumber,
            SerialNumber = request.SerialNumber,
            LastSeen = request.LastSeen,
            //AssigneeVmId = request.AssigneeVmId,
            WarehouseId = 19,
            //request.WarehouseId,
            CurrencyId = request.CurrencyId,
            PurchaseDate = request.PurchaseDate,
            Price = request.Price,
            AssigneeId = 115,
            AssigneeName = "WHIT",
            AssigneeType = "DepartmentVm",
            Leasing = request.Leasing,
            EndOfContract = request.EndOfContract,
            WarrantyUntil = request.WarrantyUntil,
            Imei = request.Imei,
            Mac = request.Mac,

        };
        _appDbContext.Assets.Add(Asset);
        await _appDbContext.SaveChangesAsync();
        return Asset.Id;
    }
}
