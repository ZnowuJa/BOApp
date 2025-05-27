using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Employees2Select.Queries;
public class GetAllEmployees2SelectQuery : IRequest<IQueryable<Employee2Select>>
{
}
public class GetAllEmployees2SelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
  : IRequestHandler<GetAllEmployees2SelectQuery, IQueryable<Employee2Select>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private IMapper _mapper = mapper;

    public async Task<IQueryable<Employee2Select>> Handle(GetAllEmployees2SelectQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Employees.Where(p => p.IsActive == 1).ToListAsync(cancellationToken);
        var res = new List<Employee2Select>();
        foreach (var item in result)
        {
            res.Add(MapToViewModel(item));
        }

        return res.AsQueryable();
    }

    private Employee2Select MapToViewModel(Employee item)
    {
        return new Employee2Select
        {
            Id = item.Id,
            FirstName = item.FirstName,
            LastName = item.FirstName,
            LongName = item.LastName,
            Email = item.Email,
            AzureObjectId = item.AzureObjectId,
            EnovaEmpId = item.EnovaEmpId,
            Position = item.Position,
            ManagerId = item.ManagerId,
            IsManager = item.IsManager,
            IsActive = item.IsActive,
            AspNetUserId = item.AspNetUserId,
        };
    }
}