using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviors;
public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly Stopwatch _stopwatch;

    public LoggingPipelineBehavior(
    ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _stopwatch = new Stopwatch();
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _stopwatch.Start();
        var requestName = typeof(TRequest).Name;
        //Request
        _logger.LogInformation(
            "Handling {Name}. {@Date}",
            requestName,
            DateTime.UtcNow);
        var result = await next();
        _stopwatch.Stop();
        var elapsed = _stopwatch.ElapsedMilliseconds;
        //Response
        _logger.LogInformation(
            "CleanArchitecture Request: {Name} {@request}. {@Date} | lasted {elapsed}",
            requestName,
            request,
            DateTime.UtcNow, elapsed);
        return result;
    }
}