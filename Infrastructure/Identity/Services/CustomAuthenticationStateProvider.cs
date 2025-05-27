using Microsoft.AspNetCore.Components.Authorization;

namespace Infrastructure.Identity.Services;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationStateProvider _innerProvider;

    public CustomAuthenticationStateProvider(AuthenticationStateProvider innerProvider)
    {
        _innerProvider = innerProvider;
    }

    public async Task RefreshAuthenticationStateAsync()
    {
        var authenticationState = await _innerProvider.GetAuthenticationStateAsync();

        NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return await _innerProvider.GetAuthenticationStateAsync();
    }
}