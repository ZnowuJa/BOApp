using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
internal class GetAllEmployeesForSelectQueryHandler : IRequestHandler<GetAllEmployeesForSelectQuery, IQueryable<EmployeeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    //private readonly Stopwatch _stopwatch;
    //private Logger _logger;
    public GetAllEmployeesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    public async Task<IQueryable<EmployeeVm>> Handle(GetAllEmployeesForSelectQuery request, CancellationToken cancellationToken)
    {

        Employee item = new Employee() { Id = 0, Email = "Select...", ManagerId = 0 };

        List<Employee> itemList = [item];
        var result = await _appDbContext.Employees.Where(p => p.StatusId == 1)
            .Include(b => b.Type)
            .ToListAsync(cancellationToken);
        itemList.AddRange(result);

        var res = _mapper.Map<List<EmployeeVm>>(itemList);

        foreach (var r in res)
        {
            var man = result
                .Where(p => p.Id == r.ManagerId)
                .FirstOrDefault();
            if (man == null)
            {
                continue;
            }
            r.Manager.Id = man.Id;
            r.Manager.FirstName = man.FirstName;
            r.Manager.LastName = man.LastName;
            r.Manager.LongName = man.FirstName + " " + man.LastName;
            r.Manager.Email = man.Email;
            r.Manager.MobileNumber = man.MobileNumber;
            r.Manager.PhoneNumber = man.PhoneNumber;
        }


        return res.AsQueryable();
    }
}

