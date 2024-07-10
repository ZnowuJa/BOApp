using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.AssetHistories.Commands;
public class CreateAssetHistoryCommandHandler : IRequestHandler<CreateAssetHistoryCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateAssetHistoryCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateAssetHistoryCommand request, CancellationToken cancellationToken)
    {
        AssetHistory AssetHistory = new()
        {
            AssetId  = request.AssetId,
            AssetTagNumber  = request.AssetTagNumber,
            Serial  = request.Serial,
            CategoryName  = request.CategoryName,
            PartName  = request.PartName,
            ChangeDate = request.ChangeDate,
            AStateName  = request.AStateName,
            ALongName  = request.ALongName,
            ATypeName  = request.ATypeName,
            AWarehouseName  = request.AWarehouseName,
            BStateName  = request.BStateName,
            BLongName  = request.BLongName,
            BTypeName  = request.BTypeName,
            BWarehouseName  = request.BWarehouseName

    };
        _appDbContext.AssetsHistory.Add(AssetHistory);
        await _appDbContext.SaveChangesAsync();
        return AssetHistory.Id;
    }
}
