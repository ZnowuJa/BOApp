using Application.Interfaces;
using Application.ViewModels.AutoStacja;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Queries;

public class CheckLicenseQuery(string licenseName) : IRequest<bool>
{
    public string LicenseName { get; set; } = licenseName;
}

public class CheckLicenseQueryHandler(IAutoStacjaDbContext autoStacjaDbContext, IMapper mapper) : IRequestHandler<CheckLicenseQuery, bool>
{
    private readonly IAutoStacjaDbContext _autoStacjaDbContext = autoStacjaDbContext;
    

    public async Task<bool> Handle(CheckLicenseQuery request, CancellationToken cancellationToken)
    {
        return await _autoStacjaDbContext.MysystemPunkts
            .AnyAsync(p => p.Nazwa == request.LicenseName, cancellationToken);
    }
}