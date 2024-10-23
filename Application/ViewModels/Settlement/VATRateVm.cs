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
    public class VATRateVm : IMapFrom<VATRate>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Percentage { get; set; }
        public string Information { get; set; }
        public int Order { get; set; }
        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VATRate, VATRateVm>().ReverseMap();
        }
    }
}
