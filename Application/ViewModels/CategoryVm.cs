using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;


namespace Application.ViewModels;

public class CategoryVm : IMapFrom<Category>
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Prefix { get; set; }
    public int LeadingZeros { get; set; }
    public int? StatusId { get; set; }
    public CategoryTypeVm? CategoryTypeVm { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryVm>()
            .ForMember(dest => dest.CategoryTypeVm, opt => opt.MapFrom(src => src.CategoryType));
        ;
    }
}
