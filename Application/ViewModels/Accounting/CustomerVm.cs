using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Mappings;

using AutoMapper;

using Domain.Entities.Accounting;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels.Accounting;
public class CustomerVm : IMapFrom<Customer>
{

    public int KontrahentId { get; set; }

    public string Nazwa { get; set; }

    public string Nip { get; set; }

    public bool Przelew { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Customer, CustomerVm>()
            .ReverseMap();
    }
}
