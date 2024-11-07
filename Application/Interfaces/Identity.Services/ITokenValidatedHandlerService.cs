using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;

namespace Application.Interfaces.Identity.Services;
public interface ITokenValidatedHandlerService
{
    Task HandleTokenValidation(TokenValidatedContext context);
    //Task HandleTokenValidation(TokenValidatedContext context);
    //Task UpdateUserOnSignIn(TokenValidatedContext ctx);
    //Task RegisterUserEmplOnSignIn(TokenValidatedContext ctx);
    //Task RefreshClaims(ClaimsPrincipal user);
}