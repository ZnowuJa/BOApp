using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels.General;
public class ManagerVm : IMapFrom<Employee>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LongName { get; set; }
    public string Email { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public int EnovaEmpId { get; set; }
    public string DeptNumber { get; set; }

    public ManagerVm()
    {
        Id = 0;
        FirstName = string.Empty;
        LastName = "Select...";
        LongName = "Select...";
        Email = string.Empty;
        MobileNumber = string.Empty;
        PhoneNumber = string.Empty;
        EnovaEmpId = 0;
        DeptNumber = string.Empty;
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, ManagerVm>()
            .ForMember(e => e.LongName, z => z.MapFrom(src2 => src2.FirstName + " " + src2.LastName));
        profile.CreateMap<EmployeeVm, ManagerVm>()
            .ForMember(e => e.LongName, z => z.MapFrom(src2 => src2.FirstName + " " + src2.LastName));
    }
}
