using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Commands;
public class UpdateEmployeeNewCommand : IRequest<int>
{
    public EmployeeVm? EmployeeVm { get; set; }

    public UpdateEmployeeNewCommand(EmployeeVm? _employeeVm)
    {
        EmployeeVm = _employeeVm;
    }

}


public class UpdateEmployeeNewCommandHandler : IRequestHandler<UpdateEmployeeNewCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdateEmployeeNewCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateEmployeeNewCommand request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Employees.Where(p => p.EnovaEmpId == request.EmployeeVm.EnovaEmpId).FirstOrDefaultAsync();

        item.FirstName = request.EmployeeVm.FirstName;
        item.LastName = request.EmployeeVm.LastName;
        item.Email = request.EmployeeVm.Email;
        item.MobileNumber = request.EmployeeVm.MobileNumber;
        item.PhoneNumber = request.EmployeeVm.PhoneNumber;
        item.ManagerId = request.EmployeeVm.ManagerId;
        item.SapNumber = request.EmployeeVm.SapNumber;
        item.DeptNumber = request.EmployeeVm.DeptNumber;
        item.PersonalDeptNumber = request.EmployeeVm.PersonalDeptNumber;

        _appDbContext.Employees.Update(item);

        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
