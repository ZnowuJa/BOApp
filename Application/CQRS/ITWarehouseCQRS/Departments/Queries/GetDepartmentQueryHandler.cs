using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Departments.Queries;
public class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetDepartmentQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<DepartmentVm> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        var departmentVm = new DepartmentVm();
        var department = await _appDbContext.Departments.Where(p => p.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        if (department != null)
        {
            var company = _appDbContext.Companies.Where(p => p.Id == department.CompanyId).FirstOrDefaultAsync(cancellationToken);
            var warehouse = _appDbContext.Warehouses.Where(p => p.Id == department.WarehouseId).FirstOrDefaultAsync(cancellationToken);
            var employee = await _appDbContext.Employees.Where(p => p.Id == department.ManagerEmpId).FirstOrDefaultAsync(cancellationToken);
            departmentVm = _mapper.Map<DepartmentVm>(department);
            departmentVm.CompanyVm = _mapper.Map<CompanyVm>(company);
            departmentVm.WarehouseVm = _mapper.Map<WarehouseVm>(warehouse);
            departmentVm.ManagerVm = _mapper.Map<EmployeeVm>(employee);

        }

        return departmentVm;
    }
}
