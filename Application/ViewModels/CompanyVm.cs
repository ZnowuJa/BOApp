using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels;
public class CompanyVm : IMapFrom<Company>
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Name { get; set; }
    public string VATID { get; set; }
    public string Street { get; set; }
    public string Building { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public string ContactPerson { get; set; }
    public string ContactPersonMobile { get; set; }
    public string ContactPersonEmail { get; set; }
    public int? StatusId { get; set; }
    public CompanyTypeVm? CompanyTypeVm { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Company, CompanyVm>().ForMember(dest => dest.CompanyTypeVm, opt => opt.MapFrom(src => src.CompanyType));
        ;
    }
}