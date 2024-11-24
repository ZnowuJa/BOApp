using Application.Interfaces;
using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels.General;
public class DepartmentVm : IMapFrom<Department>, IAssigneeVm
{
    public int Id { get; set; }
    public string DeptNumber { get; set; }
    public string LongName { get; set; }
    public int CompanyId { get; set; }
    public CompanyVm CompanyVm { get; set; }
    public string CostCenter { get; set; }
    public int WarehouseID { get; set; }
    public WarehouseVm WarehouseVm { get; set; }
    public int ManagerEmpId { get; set; }
    public EmployeeVm ManagerVm { get; set; }
    public string typeName { get; set; } = "DepartmentVm";
    public string SapNumber { get; set; }
    public void Mapping(Profile profile)
    {

        profile.CreateMap<Department, DepartmentVm>().ReverseMap();

        //Zrobić taki mapping profile, żeby przepisać dodatkowe informacje do właściwości.


    }
}
