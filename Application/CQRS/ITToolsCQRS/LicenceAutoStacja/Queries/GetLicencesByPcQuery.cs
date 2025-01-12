using Application.CQRS.AccountingCQRS.GLAccounts.Queries;
using Application.Interfaces;
using Application.ViewModels.AutoStacja;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Queries;

public class GetLicencesByPcQuery(string punktTelefon) : IRequest<string>
{
    public string licenceName{ get; set; } = punktTelefon;
}

public class GetLicencesByPcQueryHandler(IAutoStacjaDbContext autoStacjaDbContext, IMapper mapper)
    : IRequestHandler<GetLicencesByPcQuery, string>
{
    private readonly IAutoStacjaDbContext _autoStacjaDbContext = autoStacjaDbContext;

    public async Task<string> Handle(GetLicencesByPcQuery request, CancellationToken cancellationToken)
    {
        var licence = await _autoStacjaDbContext.MysystemPunkts
            .Where(x => x.PunktTelefon == request.licenceName)
            .Select(x => x.Nazwa)
            .FirstOrDefaultAsync(cancellationToken);

        if (licence == null)
        {
            throw new Exception($"Brak licencji dla komputera: {request.licenceName}");
        }

        return licence;
    }
}