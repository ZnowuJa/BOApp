using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;
public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SessionService> _logger;
    public SessionService(IHttpContextAccessor httpContextAccessor, ILogger<SessionService> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public string GetSessionId()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            _logger.LogWarning("SESSION_Service :: HttpContext is null.");
            return null; // HttpContext might be null in some scenarios
        }

        // Ensure a session is started
        var sessionId = context.Session.Id;
        _logger.LogInformation($"Session ID: {sessionId}");
        return sessionId;
    }
}