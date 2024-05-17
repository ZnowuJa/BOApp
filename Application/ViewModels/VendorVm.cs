using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels;
public class VendorVm : IMapFrom<Vendor>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? StatusId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vendor, VendorVm>().ReverseMap();
    }
}
