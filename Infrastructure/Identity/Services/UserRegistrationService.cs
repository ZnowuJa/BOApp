using Application.Entities;
using Application.Interfaces.Identity.Services;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRegistrationService(UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task CheckRoles()
    {
        string[] roles = ["User", "Manager", "Technician", "Administrator", "AppAdmin", "Accountant", "AccountantTL", "Settlement", "Compliance", "ComplianceAssistant", "Disposition", "DispositionManager", "Cashier", "AccountingNote"];
        foreach (string role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
    public async Task RegisterUserFromExternalProviderAsync(string email, string userName, string name)
    {
        // Check if the user already exists in the local database by email or any other identifier
        string firstName = name.Split(' ')[0];
        string lastName = name.Split(" ")[1];
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser == null)
        {
            // Create a new user based on information received from the external provider
            var newUser = new AppUser { UserName = userName, Email = email, FirstName = firstName, LastName = lastName };

            // Add the user to the local database
            var result = await _userManager.CreateAsync(newUser);
            if (result.Succeeded)
            {
                //assign default roles to the new user if needed
                await _userManager.AddToRoleAsync(newUser, "User");
                await _userManager.UpdateAsync(newUser);
            }
            else
            {
                Console.WriteLine(result.Errors.ToString());
            }
        }
    }
    
}
