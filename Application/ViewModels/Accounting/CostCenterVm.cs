using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting
{
    public class CostCenterVm : IMapFrom<CostCenter>
    {
        public int Id { get; set; }
        public string MPK { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CostCenter, CostCenterVm>().ReverseMap();
        }
    }
}
