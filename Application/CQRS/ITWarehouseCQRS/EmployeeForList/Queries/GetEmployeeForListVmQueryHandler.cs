using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Employees;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeForListVmQueryHandler : IRequestHandler<GetEmployeeForListVmQuery, EmployeeForListVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
    

    public GetEmployeeForListVmQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }
    public async Task<EmployeeForListVm> Handle(GetEmployeeForListVmQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Employees.Where(p => p.Id == request.EmployeeId)
            .Include(b => b.Type).FirstOrDefaultAsync();
        //var managerEmp = await _appDbContext.Employees.Where(p => p.Id == result.ManagerId).FirstOrDefaultAsync();
        var empTypeVm = new EmployeeTypeVm() 
        { 
            Id = result.Type.Id,
            Name = result.Type.Name
        };
        //var manager = new ManagerVm()
        //{
        //    Id = managerEmp.Id,
        //    FirstName = managerEmp.FirstName,
        //    LastName = managerEmp.LastName,
        //    LongName = managerEmp.FirstName + " " + managerEmp.LastName,
        //    Email = managerEmp.Email,
        //    MobileNumber = managerEmp.MobileNumber,
        //    PhoneNumber = managerEmp.PhoneNumber
        //};

        var resultVm = new EmployeeForListVm()
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            IdentityUserId = result.IdentityUserId,
            IdentityUserName = result.IdentityUserName,
            MobileNumber = result.MobileNumber,
            PhoneNumber = result.PhoneNumber,
            ManagerId = result.ManagerId,
            //Manager = manager,
            EmployeeTypeVm = empTypeVm,
            DG = result.DG,
            CC = result.CC

        };

        return resultVm;
    }
}