using Application.Entities;
using Application.Interfaces;
using Application.Interfaces.Identity.Services;
using Application.ITWarehouseCQRS.CategoryTypes.Queries;
using Domain.Entities.ITWarehouse;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
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
        //adds main roles to database
        await _userRegistrationService.CheckRoles();

        var claims = context.Principal.Claims;
        var preferredUsername = claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
        _logger.LogInformation("preferred_username : " + preferredUsername);
        // rejects any user out of @porscheinterauto.pl domain
        if (preferredUsername == null || !preferredUsername.EndsWith("@porscheinterauto.pl"))
        {
            context.Response.Redirect("/unauthorized");
            context.Fail("Access denied. Your email domain is not allowed.");
            return;
        }

        //var name = claims.FirstOrDefault(c => c.Type == "name")?.Value;

        //add roles to already registered user
        var user = await _userService.GetUserByPreferredUsername(preferredUsername);
        try
        {
            _logger.LogInformation($" Username: {user.UserName}");
        } catch (Exception ex)
        {
            _logger.LogInformation(ex.Message.ToString());
        }
        
        //var empl = await _appDbContext.Employees.Where(e => e.)
        if (user != null)
        {
            var aadoid = claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            _logger.LogInformation("USWERS ID" + aadoid);

            await UpdateUserOnSignIn(context);
            var roles = await _userService.GetUserRoles(user);
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                context.Principal.Identities.FirstOrDefault()?.AddClaim(roleClaim);
                _logger.LogInformation(role);
            }

            return;
        } else
        {
            await RegisterUserEmplOnSignIn(context);
        }

        /*
            http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress: marcin.jarco@porscheinterauto.pl
            http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname: Jarco
            http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname: Marcin
            name: Jarco Marcin (PIAPL - PL/Warsaw)
            http://schemas.microsoft.com/identity/claims/objectidentifier: 6a563daf-23d9-4827-a953-4b64d844ee11
            preferred_username: marcin.jarco@porscheinterauto.pl
            rh: 0.ASEAvmhvD_JOWkaYa-uaJQ2XiWEd8a3YYshKvABWNnUGMgYhAK0.
            http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier: KFGotiQNF2FfTgx9B7pVrVDPfiI6UPLr56PjFFnuxU0
            http://schemas.microsoft.com/identity/claims/tenantid: 0f6f68be-4ef2-465a-986b-eb9a250d9789
            uti: ctyBSGuYRUGThyxDu_8PAA
            http://schemas.microsoft.com/ws/2008/06/identity/claims/role: User
         */

        //await _userRegistrationService.RegisterUserFromExternalProviderAsync(preferredUsername, preferredUsername, name);
    }

    public async Task UpdateUserOnSignIn(TokenValidatedContext ctx)
    {
        Employee emp = new();
        //this method shall be run only for already registered users
        var preferredUsername = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
        var emailAddress = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        var displayName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        var firstName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
        var lastName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
        var aadoidc = Guid.Parse(ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value);
        _logger.LogInformation($"ctx.Principal: {preferredUsername}");
        AppUser appUser = await _userService.GetUserByPreferredUsername(preferredUsername);
        appUser.FirstName = firstName;
        appUser.LastName = lastName;
        appUser.DisplayName = displayName;
        appUser.AzureObjectId = aadoidc;
        appUser.UserName = preferredUsername;
        appUser.Email = emailAddress;
        appUser.NormalizedEmail = emailAddress.ToUpper();
        await _userService.AssignRole(appUser.Id, "User");
        //await _userService.AssignRole(appUser.Id, "AppAdmin");
        await _userService.UpdateAppUser(appUser); //updates user in AspNetUsers table
        _logger.LogInformation($"app.User: {appUser.UserName}");

        try
        {
            var employee = await _appDbContext.Employees.Where(e => e.Email.ToUpper() == appUser.Email.ToUpper()).FirstOrDefaultAsync();
            //var empInfosObj = await _appDbContext.EmpInfos.Where(e => e.Email.ToUpper() == appUser.NormalizedEmail).FirstOrDefaultAsync();
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Email = emailAddress;
            employee.LongName = firstName + " " + lastName;
            employee.AzureObjectId = aadoidc;
            employee.IdentityUserName = preferredUsername;
            employee.AspNetUserId = appUser.Id;
           
            //employee.EnovaEmpId = int.Parse(empInfosObj.EmpId);
            //employee.Position = empInfosObj.Position;
            //employee.ManagerId = empInfosObj.ManagerEmpId;
            //employee.IsManager = (empInfosObj.IsManager == 0) ? false : true;
            //employee.Type = new EmployeeType();
            //employee.IsActive = empInfosObj.IsActive;
            //employee.VcdCompanyNr = empInfosObj.VcddeptNumber.Substring(0, 5);
            //employee.VcdempId = empInfosObj.VcdempId;
            //employee.VcdempNumber = empInfosObj.VcdempNumber;
            //employee.VcddeptNumber = empInfosObj.VcddeptNumber;
            //employee.SapNumber = empInfosObj.SapnumberHr;

            _appDbContext.Employees.Update(employee);
            await _appDbContext.SaveChangesAsync();
            emp = employee;
            _logger.LogInformation($"saved employee: {employee.Email}");
        }
        catch (Exception ex) { _logger.LogInformation($"Update employee failed: {ex.Message.ToString()}"); }

        await UpdateUserClaimsOnSignIn(emp, appUser, true, ctx);


    }

    public async Task UpdateUserClaimsOnSignIn(Employee emp, AppUser user, bool update, TokenValidatedContext context)
    {
        List<Claim> claimList;
        if (update)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, claims);
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
                new Claim("Position", emp.Position),
                new Claim("ManagerId", emp.ManagerId.ToString()),
                new Claim("isManager", emp.IsManager.ToString()),
                new Claim("Type", emp.Type.Name),
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

            //foreach (var claim in claimList)
            //{
            //    Console.WriteLine($"Claim in claimSet :: type: {claim.Type}, value: {claim.Value}");
            //}

        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message, ex);
        }
            
        


        
    }

    public async Task RegisterUserEmplOnSignIn(TokenValidatedContext ctx)
    {
        string uid = string.Empty;
        Employee emp = new();
        //this method shall be run only for never registered users
        var preferredUsername = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
        var emailAddress = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        var displayName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        var firstName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
        var lastName = ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
        var aadoidc = Guid.Parse(ctx.Principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value);
        AppUser appUser = new AppUser() {
            FirstName = firstName,
            LastName = lastName,
            DisplayName = displayName,
            AzureObjectId = aadoidc,
            UserName = preferredUsername,
            Email = emailAddress,
            NormalizedEmail = emailAddress.ToUpper(),
        };
        var result = await _userManager.CreateAsync(appUser);
        
        if (result.Succeeded)
        {
            AppUser tempUser = await _userManager.FindByEmailAsync(emailAddress);
            if (tempUser != null)
            {
                uid = tempUser.Id;
            }
            //assign default roles to the new user if needed
            await _userManager.AddToRoleAsync(appUser, "User");
            await _userManager.UpdateAsync(appUser);
            
        }
        try
        {
            var employeeObj = await _appDbContext.Employees.Where(e => e.Email.ToUpper() == appUser.NormalizedEmail).FirstOrDefaultAsync();

            employeeObj.FirstName = firstName;
            employeeObj.LastName = lastName;
            employeeObj.Email = emailAddress;
            employeeObj.AzureObjectId = aadoidc;
            employeeObj.IdentityUserName = preferredUsername;
            employeeObj.Type = await _appDbContext.EmployeeTypes.Where(t => t.Name == "User").FirstOrDefaultAsync();
            employeeObj.AspNetUserId = uid;

            _appDbContext.Employees.Update(employeeObj);
            await _appDbContext.SaveChangesAsync();
        } catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }

        await UpdateUserClaimsOnSignIn(emp, appUser, false, ctx);

    }

    public Task RefreshClaims(ClaimsPrincipal user)
    {
        throw new NotImplementedException();
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