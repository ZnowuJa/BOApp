using Application.Interfaces;
using Application.ViewModels.General;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetAllEmployeesByFTEStartDateQuery : IRequest<IQueryable<EmployeeVm>>
{
    public string _fteStartDate;

    public GetAllEmployeesByFTEStartDateQuery(string fteStartDate)
    {
        _fteStartDate = fteStartDate;
    }
}

public class GetAllEmployeesByFTEStartDateQueryHandler : IRequestHandler<GetAllEmployeesByFTEStartDateQuery, IQueryable<EmployeeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllEmployeesByFTEStartDateQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }
    public async Task<IQueryable<EmployeeVm>> Handle(GetAllEmployeesByFTEStartDateQuery request, CancellationToken cancellationToken)
    {
        if (_appDbContext.Employees == null)
        {

            throw new InvalidOperationException("Employees DbSet is null");
        }
        
        var result = await _appDbContext.Employees.Where(p => p.FTEStartDate == request._fteStartDate && p.IsActive == 1 && p.VcdCompanyNr != null).Include(i => i.Type).ToListAsync(cancellationToken);

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

            return employeeVm;
        }).ToList();

        return res.AsQueryable();
    }
}