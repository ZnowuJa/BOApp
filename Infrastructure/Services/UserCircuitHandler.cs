using System.Collections.Concurrent;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Infrastructure.Services;

public class UserCircuitHandler : CircuitHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ConcurrentDictionary<string, Circuit> _activeCircuits = new();
    private readonly ConcurrentDictionary<string, string> _connectionToCircuitMap = new();

    public UserCircuitHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        var connectionId = _httpContextAccessor.HttpContext?.Features.Get<IHttpConnectionFeature>()?.ConnectionId;

        if (!string.IsNullOrEmpty(connectionId))
        {
            _activeCircuits.TryAdd(circuit.Id, circuit);
            _connectionToCircuitMap.TryAdd(connectionId, circuit.Id);
        }

        return base.OnConnectionUpAsync(circuit, cancellationToken);
    }

    public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        var connectionId = _httpContextAccessor.HttpContext?.Features.Get<IHttpConnectionFeature>()?.ConnectionId;

        if (!string.IsNullOrEmpty(connectionId))
        {
            _connectionToCircuitMap.TryRemove(connectionId, out _);
        }

        _activeCircuits.TryRemove(circuit.Id, out _);
        return base.OnConnectionDownAsync(circuit, cancellationToken);
    }

    public Circuit GetCircuit(string connectionId)
    {
        if (_connectionToCircuitMap.TryGetValue(connectionId, out var circuitId))
        {
            _activeCircuits.TryGetValue(circuitId, out var circuit);
            return circuit;
        }

        return null;
    }
}