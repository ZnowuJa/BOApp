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
    public class GLAccountVm : IMapFrom<GLAccount>
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public int? StatusId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GLAccount, GLAccountVm>().ReverseMap();
        }
    }
}
