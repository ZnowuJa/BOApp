using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.Http.Features;

namespace Infrastructure.Services
{
    public class CircuitService : ICircuitService
    {
        private readonly UserCircuitHandler _userCircuitHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CircuitService(UserCircuitHandler userCircuitHandler, IHttpContextAccessor httpContextAccessor)
        {
            _userCircuitHandler = userCircuitHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCircuitId()
        {
            var context = _httpContextAccessor.HttpContext;

            // Check for the IHttpConnectionFeature to get the connection ID
            var connectionFeature = context?.Features.Get<IHttpConnectionFeature>();

            if (connectionFeature?.ConnectionId == null)
            {
                throw new InvalidOperationException("Cannot retrieve SignalR connection ID.");
            }

            var connectionId = connectionFeature.ConnectionId;

            // Use the UserCircuitHandler to map this connection ID to a Circuit
            var circuit = _userCircuitHandler.GetCircuit(connectionId);

            return circuit?.Id ?? "Unknown Circuit";
        }
    }
}

