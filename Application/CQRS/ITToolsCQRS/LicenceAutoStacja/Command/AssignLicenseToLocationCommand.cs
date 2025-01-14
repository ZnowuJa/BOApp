/*using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITTools.LicenceAutoStacja;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Command;

public class AssignLicenseToLocationCommand(string licenceName, List<string> virtualNamesDealers) : IRequest<int>
{
    public string LicenceName { get; set; } = licenceName;
    public List<string> VirtualNamesDealers { get; set; } = virtualNamesDealers;
}

public class AssignLicenseToLocationCommandHandler(IAutoStacjaDbContext autoStacjaDbContext, IMapper mapper) : IRequestHandler<AssignLicenseToLocationCommand, int>
{
    private readonly IAutoStacjaDbContext _autoStacjaDbContext = autoStacjaDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(AssignLicenseToLocationCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Licencja: {request.LicenceName}");
        Console.WriteLine("Zaznaczone salony:");
        foreach (var dealer in request.VirtualNamesDealers)
        {
            Console.WriteLine(dealer);
        }

        var licenceName = request.LicenceName;
        var licencePrefix = GetLicencePrefix(licenceName);
        var fullLicenceName = $"{licencePrefix}_{licenceName.Substring(3)}_{licenceName.Substring(0, 2)}";

        var existingPunkt = await _autoStacjaDbContext.MysystemPunkts
            .FirstOrDefaultAsync(p => p.Nazwa == fullLicenceName);

        if (existingPunkt == null)
        {
            // Dodaj nowy punkt
            var newPunkt = new MysystemPunkt
            {
                JednostkaOrgId = null,
                Nazwa = fullLicenceName,
                PunktEmail = licenceName
            };
            _autoStacjaDbContext.MysystemPunkts.Add(newPunkt);
            await _autoStacjaDbContext.SaveChangesAsync(cancellationToken);
        }

        var licence = await _autoStacjaDbContext.MysystemPunkts
            .FirstOrDefaultAsync(p => p.Nazwa == fullLicenceName);

        if (licence == null) return 0;

        // Dodanie przełączania (analogiczne do SQL INSERT INTO MYSYSTEM_PUNKT_CON)
        foreach (var locationId in request.VirtualNamesDealers)
        {
            await AddSwitching(licence.MysystemPunktId, locationId, cancellationToken);
            await AddSwitchingBack(licence.MysystemPunktId, locationId, cancellationToken);
        }

        await _autoStacjaDbContext.SaveChangesAsync(cancellationToken);
        return 1;
        //return await Task.FromResult(Unit.Value);
    }
    private string GetLicencePrefix(string licenceName)
    {
        return licenceName.Substring(0, 3);
    }
    private async Task AddSwitching(long licenceId, string locationId, CancellationToken cancellationToken)
    {
        var exists = await _autoStacjaDbContext.MysystemPunktCons.AnyAsync(c => c.MysystemPunktInId == licenceId && c.MysystemPunktOutId.ToString() == locationId, cancellationToken);
        if (!exists)
        {
            var newSwitching = new MysystemPunktCon
            {
                MysystemPunktInId = licenceId,
                MysystemPunktOutId = long.Parse(locationId)
            };
            _autoStacjaDbContext.MysystemPunktCons.Add(newSwitching);
        }
    }

    private async Task AddSwitchingBack(long licenceId, string locationId, CancellationToken cancellationToken)
    {
        // Sprawdzanie, czy relacja powrotna już istnieje
        var exists = await _autoStacjaDbContext.MysystemPunktCons
            .AnyAsync(c => c.MysystemPunktInId == long.Parse(locationId) && c.MysystemPunktOutId == licenceId, cancellationToken);

        if (!exists)
        {
            var newSwitchingBack = new MysystemPunktCon
            {
                MysystemPunktInId = long.Parse(locationId),
                MysystemPunktOutId = licenceId
            };
            _autoStacjaDbContext.MysystemPunktCons.Add(newSwitchingBack);
        }
    }
}

public class AssignLicenseCommandHandler : IRequestHandler<AssignLicenseToLocationCommand>
{
    private readonly IAutoStacjaDbContext _context;

    public AssignLicenseToLocationCommandHandler(IAutoStacjaDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AssignLicenseToLocationCommand request, CancellationToken cancellationToken)
    {
        // Insert into MYSYSTEM_PUNKT
        var salonInfos = await _context.SalonInfo
            .Where(si => request.VirtualNamesDealers.Contains("," + si.DealerNo.ToString() + ","))
            .Select(si => new MysystemPunkt
            {
                JednostkaOrgId = si.JednostkaOrgId,
                Nazwa = si.LicencePrefix + "_" + request.LicenceName.Substring(3, request.LicenceName.Length - 3) +
                        "_" +
                        request.LicenceName.Substring(0, 2),
                PunktEmail = request.LicenceName
            })
            .ToListAsync(cancellationToken);

        var existingNames = await _context.MysystemPunkts.Select(mp => mp.Nazwa).ToListAsync(cancellationToken);
        var newPunkts = salonInfos.Where(si => !existingNames.Contains(si.Nazwa)).ToList();

        _context.MysystemPunkts.AddRange(newPunkts);
        await _context.SaveChangesAsync(cancellationToken);

        // Get LicenceID
        var licenceID = await _context.MysystemPunkts
            .Where(mp => mp.Nazwa == request.LicenceName)
            .Select(mp => mp.MysystemPunktId)
            .FirstOrDefaultAsync(cancellationToken);

        // Insert into MYSYSTEM_PUNKT_CON (switching between points)
        var punktCons = await _context.MysystemPunkts
            .Where(mp => mp.PunktEmail == request.LicenceName)
            .Select(mp => new MysystemPunktCon
            {
                MysystemPunktInId = licenceID,
                MysystemPunktOutId = mp.MysystemPunktId
            })
            .ToListAsync(cancellationToken);

        var existingPunktCons = await _context.MysystemPunktCons
            .Where(c => c.MysystemPunktInId == licenceID)
            .Select(c => c.MysystemPunktOutId)
            .ToListAsync(cancellationToken);

        var newPunktCons = punktCons
            .Where(pc => !existingPunktCons.Contains(pc.MysystemPunktOutId))
            .ToList();

        _context.MysystemPunktCons.AddRange(newPunktCons);
        await _context.SaveChangesAsync(cancellationToken);

        // Insert into MYSYSTEM_PUNKT_CON (switching back)
        var punktConsBack = await _context.MysystemPunktCons
            .Where(mp1 => mp1.MysystemPunktInId == licenceID)
            .Select(mp1 => new MysystemPunktCon
            {
                MysystemPunktInId = mp1.MysystemPunktOutId,
                MysystemPunktOutId = licenceID
            })
            .ToListAsync(cancellationToken);

        var existingPunktConsBack = await _context.MysystemPunktCons
            .Where(c => c.MysystemPunktOutId == licenceID)
            .Select(c => c.MysystemPunktInId)
            .ToListAsync(cancellationToken);

        var newPunktConsBack = punktConsBack
            .Where(pc => !existingPunktConsBack.Contains(pc.MysystemPunktInId))
            .ToList();

        _context.MysystemPunktCons.AddRange(newPunktConsBack);
        await _context.SaveChangesAsync(cancellationToken);

        // Insert into MYSYSTEM_PUNKT_CON (cross join)
        var crossJoinPunktCons = await _context.MysystemPunktCons
            .Where(mp1 => mp1.MysystemPunktInId == licenceID)
            .SelectMany(mp1 => _context.MysystemPunktCons
                .Where(mp2 => mp2.MysystemPunktInId == mp1.MysystemPunktOutId)
                .Select(mp2 => new MysystemPunktCon
                {
                    MysystemPunktInId = mp1.MysystemPunktOutId,
                    MysystemPunktOutId = mp2.MysystemPunktInId
                }))
            .ToListAsync(cancellationToken);

        var existingCrossJoinPunktCons = await _context.MysystemPunktCons
            .Select(c => new { c.MysystemPunktInId, c.MysystemPunktOutId })
            .ToListAsync(cancellationToken);

        var newCrossJoinPunktCons = crossJoinPunktCons
            .Where(pc => !existingCrossJoinPunktCons
                .Any(ec => ec.MysystemPunktInId == pc.MysystemPunktInId &&
                           ec.MysystemPunktOutId == pc.MysystemPunktOutId))
            .ToList();

        _context.MysystemPunktCons.AddRange(newCrossJoinPunktCons);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}*/