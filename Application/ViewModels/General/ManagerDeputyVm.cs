using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Administration;
using Domain.Entities.BNP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.General
{
    public class ManagerDeputyVm
    {
        public int Id { get; set; }

        public int ManagerId { get; set; }

        public string LongName { get; set; }

        public List<OrganisationRoleVm>? Deputies { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ManagerDeputy, ManagerDeputyVm>().ReverseMap();
        }
    }
}
