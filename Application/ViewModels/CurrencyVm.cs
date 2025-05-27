using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class CurrencyVm : IMapFrom<Currency>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int StatusId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Currency, CurrencyVm>().ReverseMap();
    }
}
