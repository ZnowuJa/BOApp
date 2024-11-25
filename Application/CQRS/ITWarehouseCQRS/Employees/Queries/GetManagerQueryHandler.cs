using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetManagerQueryHandler : IRequestHandler<GetManagerQuery, ManagerVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
    public GetManagerQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }
    public async Task<ManagerVm> Handle(GetManagerQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Employees.Where(p => p.Id == request.EmployeeId)
            .Include(b => b.Type).FirstOrDefaultAsync();

        //var resultVm = new EmployeeVm()
        //{
        //    Id = result.Id,
        //    FirstName = result.FirstName,
        //    LastName = result.LastName,
        //    Email = result.Email,
        //    IdentityUserId = result.IdentityUserId,
        //    IdentityUserName = result.IdentityUserName,
        //    ThirdPartyId = result.ThirdPartyId,
        //    MobileNumber = result.MobileNumber,
        //    PhoneNumber = result.PhoneNumber,
        //    Manager = manager,
        //    IsManager = result.IsManager,
        //    DG  = result.DG,
        //    CC = result.CC

        //};

        var res = _mappper.Map<ManagerVm>(result);

        return res;
    }
}