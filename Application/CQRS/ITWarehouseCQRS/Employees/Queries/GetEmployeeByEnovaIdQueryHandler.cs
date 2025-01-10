using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeByEnovaIdQueryHandler : IRequestHandler<GetEmployeeByEnovaIdQuery, EmployeeVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IPostAuthenticationService _postAuthSvc;
    public GetEmployeeByEnovaIdQueryHandler(IAppDbContext appDbContext, IMapper mapper, IPostAuthenticationService postAuthSvc)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _postAuthSvc = postAuthSvc;
    }
    public async Task<EmployeeVm> Handle(GetEmployeeByEnovaIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Employees.Where(p => p.EnovaEmpId == request.EmployeeId)
            .Include(b => b.Type).FirstOrDefaultAsync();
        var man = await _appDbContext.Employees.Where(p => p.EnovaEmpId == result.ManagerId).FirstOrDefaultAsync();
        var roles = await _postAuthSvc.GetRolesForUserAsync(result.AspNetUserId);
        //var rolesVm = roles.ToArray();
        var resEmpVm = _mapper.Map<EmployeeVm>(result);

        resEmpVm.ManagerId = (int)result.ManagerId;
        resEmpVm.Manager = _mapper.Map<ManagerVm>(man);

        resEmpVm.EmployeeTypeVm = _mapper.Map<EmployeeTypeVm>(result.Type);
        resEmpVm.Roles = roles;

        return resEmpVm;
    }
}