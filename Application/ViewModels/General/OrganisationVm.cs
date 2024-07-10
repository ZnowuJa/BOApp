using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Application.Mappings;

using AutoMapper;

using Domain.Entities.Common;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels.General;
public class OrganisationVm : IMapFrom<Organisation>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Make { get; set; }
    public string Description { get; set; }
    public string SapNumber { get; set; }
    public List<OrganisationRoleVm> Role_SalesManager { get; set; }
    public List<OrganisationRoleVm> Role_ServiceManager { get; set; }
    public List<OrganisationRoleVm> Role_DealerDirector { get; set; }
    public List<OrganisationRoleVm> Role_RegionDirector { get; set; }
    public List<OrganisationRoleVm> Role_SettlementsTeam { get; set; }

    public void Mapping(Profile profile)
    {

    }

}
