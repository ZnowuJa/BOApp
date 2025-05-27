using Application.Mappings;

using AutoMapper;

using Domain.Entities.Accounting;

namespace Application.ViewModels.Accounting;
public class CustomerVm : IMapFrom<Customer>
{

    public int KontrahentId { get; set; }

    public string Nazwa { get; set; }

    public string Nip { get; set; }

    public bool Przelew { get; set; }

    public string Numer_Fk { get; set; }
    public bool is_Firma { get; set; }
    public long Faktdoc_Id { get; set; }
    public int CC { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Customer, CustomerVm>()
            .ReverseMap();
    }
}
