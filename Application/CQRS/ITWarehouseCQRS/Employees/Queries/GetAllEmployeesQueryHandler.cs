using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IQueryable<EmployeeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllEmployeesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }
    public async Task<IQueryable<EmployeeVm>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        if (_appDbContext.Employees == null)
        {

            throw new InvalidOperationException("Employees DbSet is null");
        }

        var result = await _appDbContext.Employees.Where(p => p.IsActive == 1).Include(i => i.Type).ToListAsync(cancellationToken);

        // Create a dictionary for quick lookup of managers by their EnovaEmpId
        var managerLookup = result.ToDictionary(e => e.EnovaEmpId, e => e);

        // Map the employees to EmployeeVm and set the Manager property
        var res = result.Select(e =>
        {
            var employeeVm = _mapper.Map<EmployeeVm>(e);
            if (e.ManagerId.HasValue && managerLookup.ContainsKey(e.ManagerId.Value))
            {
                var manager = managerLookup[e.ManagerId.Value];
                employeeVm.Manager = _mapper.Map<ManagerVm>(manager);
            }
            if (e.Position == null)
            {
                employeeVm.Position = string.Empty;
            }

            return employeeVm;
        }).OrderBy(e => e.LastName).ToList();

        return res.AsQueryable();
    }
}