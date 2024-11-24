using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

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
            "Handling {Title}. {@Date}",
            requestName,
            DateTime.UtcNow);
        var result = await next();
        _stopwatch.Stop();
        var elapsed = _stopwatch.ElapsedMilliseconds;
        //Response
        //_logger.LogInformation(
        //    "CleanArchitecture Request: {Title} {@request}. {@Date} | lasted {elapsed}",
        //    requestName,
        //    request,
        //    DateTime.UtcNow, elapsed);

        _logger.LogInformation( "CleanArchitecture Request: {Title}. {@Date} | lasted {elapsed}", 
            requestName,
            DateTime.UtcNow, elapsed);
        return result;
    }
}