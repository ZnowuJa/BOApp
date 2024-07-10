using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Employees;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetManagerVmQueryHandler : IRequestHandler<GetManagerVmQuery, ManagerVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
    

    public GetManagerVmQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }
    public async Task<ManagerVm> Handle(GetManagerVmQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Employees.Where(p => p.Id == request.ManagerId)
            .Include(b => b.Type).FirstOrDefaultAsync();

        var manager = new ManagerVm()
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            LongName = result.FirstName + " " + result.LastName,
            Email = result.Email,
            MobileNumber = result.MobileNumber,
            PhoneNumber = result.PhoneNumber
        };

        return manager;
    }
}