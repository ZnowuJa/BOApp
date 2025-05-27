using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.ViewModels.General;
public class EmployeeTypeVm : IMapFrom<EmployeeType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? StatusId { get; set; }
    public EmployeeTypeVm()
    {
        Id = 0;
        Name = string.Empty;
    }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeType, EmployeeTypeVm>().ReverseMap();
    }
}
