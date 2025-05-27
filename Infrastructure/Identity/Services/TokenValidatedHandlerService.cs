using Application.Entities;
using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using Application.CQRS.ITWarehouseCQRS.CategoryTypes.Queries;
using Application.ViewModels.General;

using Domain.Entities.ITWarehouse;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Infrastructure.Identity.Services;
public class TokenValidatedHandlerService : ITokenValidatedHandlerService
{
    private readonly IUserService _userService;
    private readonly IUserRegistrationService _userRegistrationService;
    private readonly IAppDbContext _appDbContext;
    private readonly UserManager<AppUser> _userManager;

    private readonly ILogger<TokenValidatedHandlerService> _logger;

    public TokenValidatedHandlerService(UserManager<AppUser> userManager, IUserService userService, IUserRegistrationService userRegistrationService, IAppDbContext appDbContext, ILogger<TokenValidatedHandlerService> logger)
    {
        _userManager = userManager;
        _userService = userService;
        _userRegistrationService = userRegistrationService;
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task HandleTokenValidation(TokenValidatedContext context)
    {
        await _userRegistrationService.CheckRoles();
        var appUser = new AppUser();
        var empl = new Employee();
        //var userCreated = false;
        var claims = context.Principal.Claims;
        var preferredUserName = claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;

        if (! await CheckIfUserPL(claims, context))
        {
            return;
        }

        var userExists = await CheckUserInIdentityUsers(preferredUserName);
        var emplExists = await CheckUserInEmployeesTable(preferredUserName);

        if (!userExists)
        {
            userExists = await CreateIdentityUser(context);
            appUser = await _userManager.FindByEmailAsync(preferredUserName);

            if (userExists)
            {
                await _userManager.AddToRoleAsync(appUser, "User");
                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                return;
            }
            // update its claims
        } 
        else
        {
            appUser = await _userManager.FindByEmailAsync(preferredUserName);
        }

        if (!emplExists)
        {
            return;
        }
        if (emplExists && userExists)
        {
            empl = await _appDbContext.Employees.Where(e => e.Email.ToUpper() == preferredUserName.ToUpper() && e.IsActive == 1).FirstOrDefaultAsync();
            await UpdateEmployee(appUser, empl,context);
            await UpdateClaims(empl, appUser, context);
            ///update claims
        }

    }
    public async Task<bool> UpdateEmployee(AppUser appUser, Employee empl, TokenValidatedContext ctx)
    {
        //empl = await _appDbContext.Employees.Where(e => e.Email.ToString() == user.Email).FirstOrDefaultAsync();
        empl.AspNetUserId = appUser.Id;
        empl.AzureObjectId = appUser.AzureObjectId;
        if(empl.AzureObjectId.ToString() == "00000000-0000-0000-0000-000000000000")
        {
            var aadoidc = Guid.Parse(ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value);
            empl.AzureObjectId = aadoidc;
            appUser.AzureObjectId = aadoidc;
            await _userManager.UpdateAsync(appUser);
        }
        empl.IdentityUserName = appUser.UserName;
        //empl.Type = await _appDbContext.EmployeeTypes.Where(t => t.Title == "User").FirstOrDefaultAsync();
        if (empl.IsManager)
        {
            await _userManager.AddToRoleAsync(appUser, "Manager");
        }
        else
        {
            await _userManager.RemoveFromRoleAsync(appUser, "Manager");
        }
        
        _appDbContext.Employees.Update(empl);
        var res = await _appDbContext.SaveChangesAsync();
        _logger.LogInformation($"res pod SaveChangesAsync to {res}");
        return res > 0;
    }
    public async Task<bool> CreateIdentityUser (TokenValidatedContext ctx)
    {
        var preferredUsername = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
        var emailAddress = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        var displayName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        var firstName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
        var lastName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
        var aadoidc = Guid.Parse(ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value);

        AppUser appUser = new AppUser()
        {
            FirstName = firstName,
            LastName = lastName,
            DisplayName = displayName,
            AzureObjectId = aadoidc,
            UserName = preferredUsername,
            Email = emailAddress,
            NormalizedEmail = emailAddress.ToUpper(),
        };
        
        var result = await _userManager.CreateAsync(appUser);
        _logger.LogInformation($"User created: {appUser.Id} | user email: {appUser.Email}" );

        return result != null;
    }
     public async Task<bool> CheckUserInEmployeesTable(string preferedName)
    {
        var employee = await _appDbContext.Employees.Where(e => e.Email.ToString() == preferedName).FirstOrDefaultAsync();
        if (employee != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public async Task<bool> CheckUserInIdentityUsers(string preferedName)
    {
        var user = await _userService.GetUserByPreferredUsername(preferedName);
        if (user != null)
        {
            return true;
        } else
        {
            return false;
        }
    }
    public async Task<bool> CheckIfUserPL(IEnumerable<Claim> cls, TokenValidatedContext context)
    {
        var preferredUserName = cls.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
        if (preferredUserName == null || !preferredUserName.EndsWith("@porscheinterauto.pl"))
        {
            context.Response.Redirect("/unauthorized");
            context.Fail("Access denied. Your email domain is not allowed.");
            return false;
        }
        else
        {
            return true;
        }
        
    }

    public async Task<bool> UpdateClaims(Employee emp, AppUser user, TokenValidatedContext context)
    {
        List<Claim> claimList;
        string pos = string.Empty;
        if (emp.Position == null)
        {
            pos = "brak";
        } else
        {
            pos = emp.Position;
        }
        
        try
        {
            claimList = [
                new Claim("name", user.DisplayName),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("preferred_username", user.UserName),
                new Claim("email", user.Email),
                new Claim("AzureObjectId", user.AzureObjectId.ToString()),
                new Claim("EnovaEmpId", emp.EnovaEmpId.ToString()),
                new Claim("Position", pos ),
                new Claim("ManagerId", emp.ManagerId.ToString()),
                new Claim("isManager", emp.IsManager.ToString()),
                new Claim("VcdCompanyNr", emp.VcdCompanyNr),
                new Claim("VcdempId", emp.VcdempId),
                new Claim("VcdempNumber", emp.VcdempNumber),
                new Claim("VcddeptNumber", emp.VcddeptNumber),
                new Claim("SapNumber", emp.SapNumber),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("UID", emp.AspNetUserId)
                ];

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                if (role != null)
                {
                    Console.WriteLine($"UpdateUserClaims :: {role}");
                    var roleClaim = new Claim(ClaimTypes.Role, role);
                    claimList.Add(roleClaim);
                }
            };

            await _userManager.AddClaimsAsync(user, claimList);
            var appIdentity = new ClaimsIdentity(claimList);
            context.Principal.AddIdentity(appIdentity);

            _logger.LogInformation("Claims Updated!, Claims Updated!, Claims Updated!");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, ex);
            return false;
        }
    }

}


/*
 *     var roles = await _userManager.GetRolesAsync(user);
    foreach (var role in roles)
    {
        allClaims.Add(new Claim(ClaimTypes.Role, role));
    }
 * 
 */