using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Application.Mappings;

using AutoMapper;
using Domain.Entities.Accounting;

namespace Application.ViewModels.Accounting;
public class SapCostCenterVm : IMapFrom<SapCostCenter>
{
    public int Id { get; set; }
    [JsonIgnore]
    public Guid formId { get; set; } 
    public string? LocationNumber { get; set; } = string.Empty;
    public string? LocationName { get; set; } = string.Empty;
    public string? DepartmentNumber { get; set; } = string.Empty;
    public string? DepartmentName { get; set; } = string.Empty;
    public string? ResponsibleManagerName { get; set; } = string.Empty;
    public int? ResponsibleManagerEnovaId { get; set; }
    public int? ResponsibleManagerSSFId { get; set; }
    public int? StatusId { get; set; }

    public int? Share { get; set; } = 100;
    [JsonIgnore]
    public SimpleLocation Location
    {
        get
        {
            var simplLoc = new SimpleLocation()
            {
                SAPLocationNumber = LocationNumber,
                SAPLocationName = LocationName
            };
            return simplLoc;
        }
        set
        {
            LocationNumber = value.SAPLocationNumber;
            LocationName = value.SAPLocationName;
        }
    }
    [JsonIgnore]
    public SimpleDepartment Department
    {
        get
        {
            var simpleDept = new SimpleDepartment
            {
                SAPDepartmentNumber = DepartmentNumber,
                SAPDepartmentName = DepartmentName
            };
            return simpleDept;
        }
        set
        {
            DepartmentNumber = value.SAPDepartmentNumber;
            DepartmentName = value.SAPDepartmentName;

        }
    }
    [JsonIgnore]
    public List<SimpleLocation> SelectedLocations { get; set; } = new List<SimpleLocation>();
    [JsonIgnore]
    public List<SimpleDepartment> SelectedDepartments { get; set; } = new List<SimpleDepartment>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SapCostCenter, SapCostCenterVm>().ReverseMap();
    }

}

public class SimpleLocation()
{
    public string SAPLocationNumber { get; set; }
    public string SAPLocationName { get; set; }
}
public class SimpleDepartment()
{
    public string SAPDepartmentNumber { get; set; }
    public string SAPDepartmentName { get; set; }
}

