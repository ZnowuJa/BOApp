using Application.Entities;
using Application.Interfaces.Identity.Services;
using Domain.Entities.ITWarehouse;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    


    public UserService(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }
    public async Task<AppUser> GetUserByPreferredUsername(string preferredUsername)
    {
        var user = await _userManager.FindByNameAsync(preferredUsername);
        //_logger.Information("From userservice : user : " +  user + preferredUsername);
        return user;
    }
    public async Task<AppUser> GetUserByAspNetUserId(string aspNetUserId)
    {
        var user = await _userManager.FindByIdAsync(aspNetUserId);
        //_logger.Information("From userservice : user : " +  user + preferredUsername);
        return user;
    }
    public async Task<IdentityResult> RegisterUserEmplAsync(AppUser user, string password)
    {
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            // Assign default role to the registered user
            await _userManager.AddToRoleAsync(user, "User");
        }
        return result;
    }
    public async Task<List<string>> GetUserRoles(AppUser user)
    {
        var rl = await _userManager.GetRolesAsync(user);
        return (List<string>)rl;
    }
    public async Task<List<AppUser>> GetUsers()
    {
        return await _userManager.Users.ToListAsync();
    }
    public async Task<List<String>> GetRoles()
    {
        return await _roleManager.Roles.Select(x => x.Name).ToListAsync();
    }
    public async Task<IdentityResult> UpdateAppUser(AppUser user)
    {
        return await _userManager.UpdateAsync(user);
    }
    //public async Task<List<String>> GetUserRoles(AppUser user)
    //{
    //    List<string> roles = new List<String>();

    //    roles = await _userManager.GetRolesAsync(user);
    //    return OK(roles);
    //}

    public async Task<bool> AssignRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);

        return result.Succeeded;
    }
    public async Task SignOut()
    {
         await _signInManager.SignOutAsync();
    }
    public async Task AddClaimsFromEmp(Employee emp, AppUser appUser, TokenValidatedContext ctx)
    {
        var existingClaims = await _userManager.GetClaimsAsync(appUser);

        //foreach (var claim in existingClaims)
        //{
        //    if (claim == null)
        //    {
        //        //here create and add claims
        //    } else
        //    { 
        //        await _userManager.ReplaceClaimAsync(appUser, claim);
        //    }
        //}
        //// Check if the claim already exists
        ////var existingClaim = existingClaims.FirstOrDefault(c => c.Type == claim.Type);

        //if (existingClaim != null)
        //{
        //    // Update the existing claim
        //    await _userManager.ReplaceClaimAsync(user, existingClaim, claim);
        //}
        //else
        //{
        //    // Add the new claim
        //    await _userManager.AddClaimAsync(user, claim);
        //}
    }
}
