using Application.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BOAppFluentUI;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet("refreshclaims")]
    public async Task<IActionResult> RefreshClaims()
    {
        var user = await _signInManager.UserManager.GetUserAsync(User);
        await _signInManager.RefreshSignInAsync(user);
        return Ok();
    }
}
