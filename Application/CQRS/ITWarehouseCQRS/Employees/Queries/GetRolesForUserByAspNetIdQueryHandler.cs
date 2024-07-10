using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetRolesForUserByAspNetIdQueryHandler : IRequestHandler<GetRolesForUserByAspNetIdQuery, List<string>>
{
    private readonly IAppDbContext _appDbContext;
    private IPostAuthenticationService _postAuthSvc;

    public GetRolesForUserByAspNetIdQueryHandler(IAppDbContext appDbContext, IMapper mapper, IPostAuthenticationService postAuthSvc)
    {
        _appDbContext = appDbContext;
        _postAuthSvc = postAuthSvc;
    }

    public async Task<List<string>> Handle(GetRolesForUserByAspNetIdQuery request, CancellationToken cancellationToken)
    {
        List<string> roles = new List<string>();
        roles = await _postAuthSvc.GetRolesForUserAsync(request.EmployeeId);
        Console.WriteLine("Roles count :: " + roles.Count);
        foreach (var role in roles)
        { Console.WriteLine(role); }
        return roles;
    }
}
