using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Queries;

public class GetLicenseConnectionsQuery(string licenseName) : IRequest<string>
{
    public string LicenseName { get; set; } = licenseName;
}
public class GetLicenseConnectionsQueryHandler(IAutoStacjaDbContext context) : IRequestHandler<GetLicenseConnectionsQuery, string>
{
    private readonly IAutoStacjaDbContext _context = context;

    public async Task<string> Handle(GetLicenseConnectionsQuery request, CancellationToken cancellationToken)
    {
        var punkt = await _context.MysystemPunkts
            .FirstOrDefaultAsync(p => p.Nazwa == request.LicenseName, cancellationToken) ?? throw new Exception($"Brak licencji: {request.LicenseName}");

        var connections = await _context.MysystemPunktCons
            .Where(pc => pc.MysystemPunktInId == punkt.MysystemPunktId)
            .Join(_context.MysystemPunkts,
                pc => pc.MysystemPunktOutId,
                pout => pout.MysystemPunktId,
                (pc, pout) => pout.Nazwa)
            .ToListAsync(cancellationToken);

        return string.Join("\n", connections);
    }
}