using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModelsForForms;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.Configuration;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeGridModelListQueryHandler : IRequestHandler<GetEmployeeGridModelListQuery, IQueryable<EmployeeGridModel>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
    public GetEmployeeGridModelListQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }
    public async Task<IQueryable<EmployeeGridModel>> Handle(GetEmployeeGridModelListQuery request, CancellationToken cancellationToken)
    {
        List<EmployeeGridModel> empList = new List<EmployeeGridModel>();

        var results = await _appDbContext.Employees.Where(p => p.StatusId == 1)
            .Include(b => b.Type).ToListAsync(cancellationToken);

        foreach (var result in results)
        {   
            //Employee e = await _appDbContext.Employees.Where(p => p.Id == result.ManagerId).Include(b => b.Type).FirstOrDefaultAsync();
            //var xman = _mapper.Map<ManagerVm>(e);
            var resultVm = new EmployeeGridModel()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                LongName = result.FirstName + " " + result.LastName,
                Email = result.Email,
                ManagerId = result.ManagerId,
                Manager = _mappper.Map<ManagerVm>(await _appDbContext.Employees.Where(p => p.Id == result.ManagerId).Include(b => b.Type).FirstOrDefaultAsync()),
                EmployeeTypeVm = _mappper.Map<EmployeeTypeVm>(result.Type),
                DG = result.DG,
                CC = result.CC

            };
            empList.Add(resultVm);
        }

        return empList.AsQueryable();
    }
}