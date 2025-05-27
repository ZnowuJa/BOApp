using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class UnitVm : IMapFrom<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public int? StatusId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Unit, UnitVm>().ReverseMap();
    }
}
