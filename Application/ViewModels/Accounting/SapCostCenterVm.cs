using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Mappings;

using AutoMapper;
using Domain.Entities.Accounting;

namespace Application.ViewModels.Accounting;
public class SapCostCenterVm : IMapFrom<SapCostCenter>
{
    public int Id { get; set; }
    public string? LocationNumber { get; set; } = string.Empty;
    public string? LocationName { get; set; } = string.Empty;
    public string? DepartmentNumber { get; set; } = string.Empty;
    public string? DepartmentName { get; set; } = string.Empty;
    public string? ResponsibleManagerName { get; set; } = string.Empty;
    public int? ResponsibleManagerEnovaId { get; set; }
    public int? ResponsibleManagerSSFId { get; set; }
    public int? StatusId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SapCostCenter, SapCostCenterVm>().ReverseMap();
    }

}

public class SimpleLocation()
{
    public string LocationNumber { get; set; }
    public string LocationName { get; set; }
}
public class SimpleDepartment()
{
    public string DepartmentNumber { get; set; }
    public string DepartmentName { get; set; }
}

