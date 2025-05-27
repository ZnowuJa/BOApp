using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class WarehouseVm : IMapFrom<Warehouse>
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public int? StatusId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Warehouse, WarehouseVm>().ReverseMap();
    }
}
