using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.Interfaces;
public class AssigneeVmToAssigneeConverter : ITypeConverter<IAssigneeVm, IAssignee>
{
    private readonly IMapper _mapper;

    public AssigneeVmToAssigneeConverter(IMapper mapper)
    {
        _mapper = mapper;
    }
    public IAssignee Convert(IAssigneeVm source, IAssignee destination, ResolutionContext context)
    {
        // Check the actual type of source and map it to the corresponding IAssignee implementation
        if (source is DepartmentVm departmentVm)
        {
            return _mapper.Map<Department>(departmentVm);
        }
        else if (source is EmployeeVm employeeVm)
        {
            return _mapper.Map<Employee>(employeeVm);
        }
        else
        {
            throw new InvalidOperationException("Unsupported type of IAssigneeVm");
        }
    }
}