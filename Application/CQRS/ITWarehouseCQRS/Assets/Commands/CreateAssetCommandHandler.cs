using System.Runtime.InteropServices;

using Application.DTOs;
using Application.Interfaces;
using Application.ITWarehouseCQRS.AssetHistories.Commands;
using Application.ITWarehouseCQRS.Parts.Queries;
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
            WarehouseId = 19,
            Price = request.Price,
            CurrencyId = request.CurrencyId,
            PurchaseDate = request.PurchaseDate,
            AssigneeId = 115,
            AssigneeName = "WHIT",
            AssigneeType = "DepartmentVm",
            Leasing = request.Leasing,
            EndOfContract = request.EndOfContract,
            WarrantyUntil = request.WarrantyUntil,
            Imei = request.Imei,
            Mac = request.Mac,
            EndOfSupport = request.EndOfSupport,

        };
        _appDbContext.Assets.Add(Asset);
        await _appDbContext.SaveChangesAsync();
        await AddHistory(Asset, request.ModifiedBy);
        return Asset.Id;
    }

    private async Task AddHistory(Asset a, string? mod)
    {
        var part = await _appDbContext.Parts.Where(p => p.Id == a.PartId)
            .Include(i => i.Vendor)
            .Include(i => i.Category)
            .FirstOrDefaultAsync();

        var ah = new AssetHistory();
        ah.AssetId = a.Id.ToString();
        ah.AssetTagNumber = a.AssetTagNumber;
        ah.Serial = a.SerialNumber;
        ah.CategoryName = part.Category.Name;
        ah.PartName = part.Name;
        ah.ChangeDate = DateTime.Now;
        ah.AStateName = string.Empty;
        ah.ALongName = string.Empty;
        ah.ATypeName = string.Empty;
        ah.AWarehouseName = string.Empty;
        ah.BStateName = "New";
        ah.BLongName = a.AssigneeName;
        ah.BTypeName = a.AssigneeType;
        ah.BWarehouseName = "WHIT";
        ah.ModifiedBy = mod;
        _appDbContext.AssetsHistory.Add(ah);
        await _appDbContext.SaveChangesAsync();
    }
}
