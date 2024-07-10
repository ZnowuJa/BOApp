using Application.Mappings;

using AutoMapper;

using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class AssetHistoryVm : IMapFrom<AssetHistory>
{
    public int Id { get; set; }
    public string AssetId { get; set; }
    public string AssetTagNumber { get; set; }
    public string Serial { get; set; }
    public string CategoryName { get; set; }
    public string PartName { get; set; }
    public DateTime ChangeDate { get; set; } = DateTime.Now;
    public string AStateName { get; set; }
    public string ALongName { get; set; }
    public string ATypeName { get; set; }
    public string? AWarehouseName { get; set; }
    public string BStateName { get; set; }
    public string BLongName { get; set; }
    public string BTypeName { get; set; }
    public string? BWarehouseName { get; set; }
    public string? ModifiedBy { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<AssetHistory, AssetHistoryVm>().ReverseMap();
    }
}