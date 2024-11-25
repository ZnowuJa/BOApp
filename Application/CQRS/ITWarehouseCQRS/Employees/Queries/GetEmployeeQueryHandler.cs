using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IPostAuthenticationService _postAuthSvc;
    public GetEmployeeQueryHandler(IAppDbContext appDbContext, IMapper mapper, IPostAuthenticationService postAuthSvc)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _postAuthSvc = postAuthSvc;
    }
    public async Task<EmployeeVm> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Employees.Where(p => p.Id == request.EmployeeId)
            .Include(b => b.Type).FirstOrDefaultAsync();
        var man = await _appDbContext.Employees.Where(p => p.EnovaEmpId == result.ManagerId).FirstOrDefaultAsync();
        var roles = await _postAuthSvc.GetRolesForUserAsync(result.AspNetUserId);
        //var rolesVm = roles.ToArray();
        var resultVm = new EmployeeVm()
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            AzureObjectId = result.AzureObjectId,
            IdentityUserName = result.IdentityUserName,
            ThirdPartyId = result.ThirdPartyId,
            MobileNumber = result.MobileNumber,
            PhoneNumber = result.PhoneNumber,
            ManagerId = (int)result.ManagerId,
            Manager = _mapper.Map<ManagerVm>(man),
            IsManager = result.IsManager,
            EmployeeTypeVm = _mapper.Map<EmployeeTypeVm>(result.Type),
            Roles = roles
        };
        Console.WriteLine($"FROM CQRS after ass:: employeeId : {resultVm.Id}, empmanId : {resultVm.ManagerId}, while man : {resultVm.Manager.Id}");
        //var res = _mapper.Map<EmployeeVm>(result);
        //Console.WriteLine($"FROM CQRS after mapper:: employeeId : {res.Id}, empmanId : {res.ManagerId}, while man : {res.Manager.Id}");
        return resultVm;
    }
}