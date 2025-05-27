using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Employees;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Employees.Queries;
internal class GetAllManagerVmForSelectQueryHandler : IRequestHandler<GetAllManagerVmForSelectQuery, IQueryable<ManagerVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllManagerVmForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    public async Task<IQueryable<ManagerVm>> Handle(GetAllManagerVmForSelectQuery request, CancellationToken cancellationToken)
    {
        ManagerVm emp = new ManagerVm() { Id = 0, Email = "Select..." };

        List<ManagerVm> itemList = new List<ManagerVm>();
        itemList.Add(emp);

        var result = await _appDbContext.Employees
            .Where(p => p.StatusId == 1)
            .Where(q => q.IsManager == true)
            .Include(i => i.Type)
            .ToListAsync(cancellationToken);
        foreach (var res in result)
        {
            var resItem = new ManagerVm()
            {
                Id = res.Id,
                FirstName = res.FirstName,
                LastName = res.LastName,
                LongName = res.FirstName + " " + res.LastName,
                Email = res.Email,
                MobileNumber = res.MobileNumber,
                PhoneNumber = res.PhoneNumber

            };

            itemList.Add(resItem);

        }
 

        return itemList.AsQueryable();
    }
}

