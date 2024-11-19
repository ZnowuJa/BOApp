using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;
public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetSessionId()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
            return null; // HttpContext might be null in some scenarios
        }

        // Ensure a session is started
        context.Session.SetString("SessionStarted", "true");
        return context.Session.Id;
    }
}