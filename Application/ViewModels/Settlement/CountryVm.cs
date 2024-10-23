using Application.Mappings;
using AutoMapper;
using Domain.Entities.Settlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Settlement
{
    public class CountryVm : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsEU { get; set; }
        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryVm>().ReverseMap();
        }
    }
}
