using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.AssetHistories.Commands;
public class CreateAssetHistoryCommand : IRequest<int>
{
    public string AssetId { get; set; }
    public string AssetTagNumber { get; set; }
    public string Serial { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string PartName { get; set; }
    public DateTime ChangeDate { get; set; }
    public string AStateName { get; set; }
    public string ALongName { get; set; }
    public string ATypeName { get; set; }
    public string? AWarehouseName { get; set; }
    public string BStateName { get; set; }
    public string BLongName { get; set; }
    public string BTypeName { get; set; }
    public string? BWarehouseName { get; set; }


    public CreateAssetHistoryCommand(
        string assetId,
        string assetTagNumber,
        string serial,
        string categoryName,
        string partName,
        DateTime changeDate,
        string aStateName,
        string aLongName,
        string aTypeName,
        string aWarehouseName,
        string bStateName,
        string bLongName,
        string bTypeName,
        string bWarehouseName
        )
    {
        AssetId = assetId;
        AssetTagNumber = assetTagNumber;
        Serial = serial;
        CategoryName = categoryName;
        PartName = partName;
        ChangeDate = changeDate;
        AStateName = aStateName;
        ALongName = aLongName;
        ATypeName = aTypeName;
        AWarehouseName = aWarehouseName;
        BStateName = bStateName;
        BLongName = bLongName;
        BTypeName = bTypeName;
        BWarehouseName = bWarehouseName;
    }
}
public class CreateAssetHistoryCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<CreateAssetHistoryCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(CreateAssetHistoryCommand request, CancellationToken cancellationToken)
    {
        AssetHistory AssetHistory = new()
        {
            AssetId = request.AssetId,
            AssetTagNumber = request.AssetTagNumber,
            Serial = request.Serial,
            CategoryName = request.CategoryName,
            PartName = request.PartName,
            ChangeDate = request.ChangeDate,
            AStateName = request.AStateName,
            ALongName = request.ALongName,
            ATypeName = request.ATypeName,
            AWarehouseName = request.AWarehouseName,
            BStateName = request.BStateName,
            BLongName = request.BLongName,
            BTypeName = request.BTypeName,
            BWarehouseName = request.BWarehouseName

        };
        _appDbContext.AssetsHistory.Add(AssetHistory);
        await _appDbContext.SaveChangesAsync();
        return AssetHistory.Id;
    }
}