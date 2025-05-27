using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class PartVm : IMapFrom<Part>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryVm CategoryVm { get; set; }
    public VendorVm VendorVm { get; set; }
    public string? Description { get; set; }
    public string? Photo { get; set; }
    public int WarrantyPeriod { get; set; }
    public bool isCurrentlyOrderable { get; set; }
    public DateTime? EndOfSupport { get; set; }
    public void Mapping(Profile profile)
    {
        
        profile.CreateMap<Part, PartVm>()
            .ForMember(d => d.CategoryVm, s => s.MapFrom(src => src.Category))
            .ForMember(e => e.VendorVm, z => z.MapFrom(src2 => src2.Vendor));

        profile.CreateMap<PartVm, Part>()
            .ForMember(d => d.Category, s => s.MapFrom(src => src.CategoryVm))
            .ForMember(e => e.Vendor, z => z.MapFrom(src2 => src2.VendorVm));
    }
}
