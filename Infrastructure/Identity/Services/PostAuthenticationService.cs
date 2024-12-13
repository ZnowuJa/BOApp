
using Application.Entities;
using Application.Interfaces.Identity.Services;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Identity.Services;

public class PostAuthenticationService : IPostAuthenticationService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public PostAuthenticationService(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<string>> GetRolesForUserAsync(string userId)
    {
        try
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
        
            if (user != null)
            {
                var rolesy = await _userManager.GetRolesAsync(user);
                return new List<string>(rolesy);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return new List<string>(); // If user not found or has no roles, return an empty list
    }

    public async Task UpdateRolesForUserAsync(AppUser user, string[] newRoles)
    {
        await _userManager.RemoveFromRolesAsync(user, newRoles);
        await _userManager.AddToRolesAsync(user, newRoles);
    }

    //public async Task AssignDefaultRoleIfNotExistAsync(AppUser user)
    //{
    //    // Check if the user exists in the local database
    //    var existingUser = await _userManager.FindByEmailAsync(user.Email);

    //    if (existingUser == null)
    //    {
    //        // Create the user in the local database
    //        await _userManager.CreateAsync(user);

    //        // Assign default "User" role to the newly created user
    //        await _userManager.AddToRoleAsync(user, "User");
    //    }
    //}

    // Other methods for local user management and role assignment...

}
