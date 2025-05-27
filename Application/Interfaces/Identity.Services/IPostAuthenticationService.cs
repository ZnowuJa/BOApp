using Application.Entities;

namespace Application.Interfaces.Identity.Services;
public interface IPostAuthenticationService
{
    
    Task<List<string>> GetRolesForUserAsync(string userId);
    Task UpdateRolesForUserAsync(AppUser user, string[] newRoles);
}