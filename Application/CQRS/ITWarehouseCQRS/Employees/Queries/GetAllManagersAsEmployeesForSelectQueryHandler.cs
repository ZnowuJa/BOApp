using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
internal class GetAllManagersAsEmployeesForSelectQueryHandler : IRequestHandler<GetAllManagersAsEmployeesForSelectQuery, IQueryable<EmployeeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllManagersAsEmployeesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    public async Task<IQueryable<EmployeeVm>> Handle(GetAllManagersAsEmployeesForSelectQuery request, CancellationToken cancellationToken)
    {
        Employee item = new Employee();

        List<Employee> itemList = [item];
        var result = await _appDbContext.Employees
            .Where(p => p.StatusId == 1)
            .Where(q => q.IsManager == true)
            .Include(b => b.Type)
            .ToListAsync(cancellationToken);
        itemList.AddRange(result);
        var result2 = await _appDbContext.Employees
            .Where(p => p.IsActive == 1)
            .Where(q => q.SapNumber == "157892")
            .Include(b => b.Type)
            .ToListAsync(cancellationToken);
        itemList.AddRange(result2);

        //foreach(var emp in  itemList)
        //{
        //    emp.Manager = await _appDbContext.Employees.Where(p => p.Id == emp.Manager.Id).FirstOrDefaultAsync();

        //}
        Console.WriteLine($"FROM CQRS:: after result count : {result.Count}");
        var res = _mapper.Map<List<EmployeeVm>>(itemList);
        Console.WriteLine($"FROM CQRS:: after result and mappwr count : {res.Count}");
        return res.AsQueryable();
    }
}

