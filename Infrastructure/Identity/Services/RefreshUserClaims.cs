using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services;

public class RefreshUserClaims : IClaimsTransformation
{
    private readonly UserManager<AppUser> _userManager;

    public RefreshUserClaims(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;

        if (identity == null || !identity.IsAuthenticated)
            return principal;

        var email = principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
            return principal;

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return principal;

        // ✅ Remove all existing role claims
        var existingRoleClaims = identity.FindAll(ClaimTypes.Role).ToList();
        foreach (var oldRole in existingRoleClaims)
        {
            identity.RemoveClaim(oldRole);
        }

        // ✅ Add up-to-date roles
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return principal;
    }
}
//public class RefreshUserClaims : IClaimsTransformation
//{
//    private readonly UserManager<AppUser> _userManager;

//    public RefreshUserClaims(UserManager<AppUser> userManager)
//    {
//        _userManager = userManager;
//    }

//    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
//    {
//        var identity = (ClaimsIdentity)principal.Identity;
//        var email = principal.FindFirstValue(ClaimTypes.Email);
//        var user = await _userManager.FindByEmailAsync(email);

//        if (user != null)
//        {
//            var roles = await _userManager.GetRolesAsync(user);

//            var existingRoleClaims = identity.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
//            foreach (var claim in existingRoleClaims)
//            {
//                identity.RemoveClaim(claim);
//            }

//            foreach (var role in roles)
//            {
//                identity.AddClaim(new Claim(ClaimTypes.Role, role));
//            }
//        }

//        return principal;
//    }
//}

