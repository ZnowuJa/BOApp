using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Departments.Queries;
public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IQueryable<DepartmentVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetAllDepartmentsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<IQueryable<DepartmentVm>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        List<DepartmentVm> itemList = new List<DepartmentVm>();
        var result = await _appDbContext.Departments
            .Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
        itemList = _mapper.Map<List<DepartmentVm>>(result);
        if(itemList.Count > 0)
        {
            var itemsCompanies = await _appDbContext.Companies.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
            var itemsWarehouses = await _appDbContext.Warehouses.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
            var itemsManagers = await _appDbContext.Employees.Where(q => q.StatusId == 1 && q.IsActive == 1).ToListAsync(cancellationToken);
            //.Where(p => p.IsManager == true)

            foreach (var item in itemList)
            {
                //Department dept = new Department();
                //var deptVm = _mapper.Map<DepartmentVm>(item);
                item.CompanyVm = _mapper.Map<CompanyVm>( itemsCompanies.Where(c => c.Id == item.CompanyId).FirstOrDefault());
                item.WarehouseVm = _mapper.Map<WarehouseVm>(itemsWarehouses.Where(c => c.Id == item.WarehouseID).FirstOrDefault());
                var manager = itemsManagers.Where(c => c.EnovaEmpId == item.ManagerEmpId).FirstOrDefault();
                var managerVm = _mapper.Map<EmployeeVm>(manager);
                if (managerVm == null)
                {
                    managerVm = new EmployeeVm();
                }
                    item.ManagerVm = managerVm;

            }

        }
        return itemList.AsQueryable();
    }
}
