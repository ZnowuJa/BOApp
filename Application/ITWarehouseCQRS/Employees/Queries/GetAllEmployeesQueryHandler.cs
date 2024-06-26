﻿using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IQueryable<EmployeeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    
    public GetAllEmployeesQueryHandler(IAppDbContext appDbContext, IMapper mapper )
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }
    public async Task<IQueryable<EmployeeVm>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
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

            return employeeVm;
        }).ToList();

        return res.AsQueryable();
    }
}