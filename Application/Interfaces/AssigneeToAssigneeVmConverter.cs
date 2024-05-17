using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;

namespace Application.Interfaces;
public class AssigneeToAssigneeVmConverter : ITypeConverter<IAssignee, IAssigneeVm>
{
    private readonly IMapper _mapper;

    public AssigneeToAssigneeVmConverter(IMapper mapper)
    {
        _mapper = mapper;
    }
    public IAssigneeVm Convert(IAssignee source, IAssigneeVm destination, ResolutionContext context)
    {
        if (source is Department department)
        {
            return _mapper.Map<DepartmentVm>(department);
        }
        else if (source is Employee employee)
        {
            return _mapper.Map<EmployeeVm>(employee);
        }
        else
        {
            throw new InvalidOperationException("Unsupported type of IAssignee");
        }



    }
}
