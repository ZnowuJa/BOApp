using Application.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Identity.Services;
public interface IUserService
{
    Task<bool> AssignRole(string userId, string roleName);
    Task<List<string>> GetRoles();
    Task<AppUser> GetUserByAspNetUserId(string aspNetUserId);
    Task<AppUser> GetUserByPreferredUsername(string preferredUsername);
    Task<List<string>> GetUserRoles(AppUser user);
    Task<List<AppUser>> GetUsers();
    Task<IdentityResult> RegisterUserEmplAsync(AppUser user, string password);
    Task<IdentityResult> UpdateAppUser(AppUser user);
    Task SignOut();

}