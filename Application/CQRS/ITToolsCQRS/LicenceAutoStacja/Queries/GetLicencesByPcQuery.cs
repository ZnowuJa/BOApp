using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Queries;

public class GetLicencesByPcQuery(string punktTelefon) : IRequest<string>
{
    public string LicenceName { get; set; } = punktTelefon;
}

public class GetLicencesByPcQueryHandler(IAutoStacjaDbContext autoStacjaDbContext, IMapper mapper)
    : IRequestHandler<GetLicencesByPcQuery, string>
{
    private readonly IAutoStacjaDbContext _autoStacjaDbContext = autoStacjaDbContext;

    public async Task<string> Handle(GetLicencesByPcQuery request, CancellationToken cancellationToken)
    {
        var licence = await _autoStacjaDbContext.MysystemPunkts
            .Where(x => x.PunktTelefon == request.LicenceName)
            .Select(x => x.Nazwa)
            .FirstOrDefaultAsync(cancellationToken);

        return licence;
            /*?? throw new Exception($"Brak licencji dla komputera: {request.LicenceName}")*/
    }
}