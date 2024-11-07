using Application.Mappings;
using AutoMapper;
using Domain.Entities.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting
{
    public class CostCenterVm : IMapFrom<CostCenter>
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string MPK { get; set; }
        [Required]
        [MinLength(3)]
        public string Description { get; set; }
        [Required]
        [MinLength(3)]
        public string Text { get; set; }
        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CostCenter, CostCenterVm>().ReverseMap();
        }
    }
}
