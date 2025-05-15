using Application.Interfaces;
using Application.ViewModels.AutoStacja;
using AutoMapper;
using Domain.Entities.ITTools.LicenceAutoStacja;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Command;

public class AssignLicenseToLocationCommand(string licenceName, List<int> virtualIdDealers) : IRequest<AssignLicenseToLocationResult>
{
    public string LicenceName { get; set; } = licenceName;
    public List<int> VirtualIdDealers { get; set; } = virtualIdDealers;
}

public class AssignLicenseToLocationResult
{
    public int AssignedCount { get; set; }
    public List<string> Messages { get; set; } = new();
}

public class AssignLicenseToLocationCommandHandler(IAutoStacjaDbContext autoStacjaDbContext, IDiscounts2AS discounts2AS, IMapper mapper) : IRequestHandler<AssignLicenseToLocationCommand, AssignLicenseToLocationResult>
{
    private readonly IAutoStacjaDbContext _autoStacjaDbContext = autoStacjaDbContext;
    private readonly IDiscounts2AS _discounts2AS = discounts2AS;
    private readonly IMapper _mapper = mapper;

    public async Task<AssignLicenseToLocationResult> Handle(AssignLicenseToLocationCommand request, CancellationToken cancellationToken)
    {
        var baseLicence = await _autoStacjaDbContext.MysystemPunkts
        .FirstOrDefaultAsync(p => p.Nazwa == request.LicenceName, cancellationToken);

        var result = new AssignLicenseToLocationResult();

        if (baseLicence == null)
        {
            var msg = $"Nie znaleziono głównej licencji '{request.LicenceName}', operacja przerwana.";
            Console.WriteLine(msg);
            result.Messages.Add(msg);
            return result;
        }

        Console.WriteLine($"Licencja: {request.LicenceName}");
        Console.WriteLine("Zaznaczone salony:");

        foreach (var dealerId in request.VirtualIdDealers)
        {
            Console.WriteLine($"Dealer ID: {dealerId}");
        }

        var newPoints = new List<MysystemPunkt>();
        foreach (var dealerId in request.VirtualIdDealers)
        {
            // Pobierz informacje o salonie z bazy Discounts2AS
            var salonInfo = await GetSalonInfoByDealerId(dealerId, cancellationToken);
            if (salonInfo == null)
            {
                Console.WriteLine($"Nie znaleziono informacji o salonie dla dealera o ID {dealerId}. Pomiń ten salon.");
                continue;
            }

            // Skonstruowanie pełnej nazwy licencji
            var fullLicenceName = $"{salonInfo.LicencePrefix}_{request.LicenceName.Substring(3)}_{request.LicenceName.Substring(0, 2)}";

            if (salonInfo.JednostkaOrg == baseLicence.JednostkaOrgId)
            {
                var msg = $"Pomijam '{fullLicenceName}' — dealer {dealerId} ma tę samą JednostkaOrgId ({salonInfo.JednostkaOrg}) co główna licencja.";
                Console.WriteLine(msg);
                result.Messages.Add(msg);
                continue;
            }

            // Sprawdzenie, czy licencja już istnieje
            var existingLicense = await _autoStacjaDbContext.MysystemPunkts
                .FirstOrDefaultAsync(p => p.Nazwa == fullLicenceName, cancellationToken);

            if (existingLicense != null)
            {
                var msg = $"Licencja '{fullLicenceName}' już istnieje.";
                Console.WriteLine(msg);
                result.Messages.Add(msg);
                continue;
            }

            // Dodanie nowego punktu licencji
            var newPunkt = new MysystemPunkt
            {
                JednostkaOrgId = salonInfo.JednostkaOrg,  // Pobieramy JednostkaOrgId z salonInfo
                Nazwa = fullLicenceName,
                PunktEmail = request.LicenceName
            };

            _autoStacjaDbContext.MysystemPunkts.Add(newPunkt);
            await _autoStacjaDbContext.SaveChangesAsync(cancellationToken);

            // Pobieramy nowo dodany punkt, aby mieć jego ID
            var addedPoint = await _autoStacjaDbContext.MysystemPunkts
                .FirstOrDefaultAsync(p => p.Nazwa == fullLicenceName, cancellationToken);

            if (addedPoint == null) continue;

            newPoints.Add(addedPoint);
        }

        foreach (var point1 in newPoints.Append(baseLicence))
        {
            foreach (var point2 in newPoints.Append(baseLicence))
            {
                // Pomijamy połączenie, jeśli JednostkaOrgId jest takie samo
                if (point1.JednostkaOrgId == point2.JednostkaOrgId)
                {
                    Console.WriteLine($"Pomiń połączenie między {point1.Nazwa} a {point2.Nazwa}, bo mają tę samą JednostkaOrgId.");
                    continue;  // Pomiń dalsze operacje dla tego przypadku
                }
                // Sprawdzamy, czy MysystemPunktId są różne (aby nie tworzyć połączenia z samym sobą)
                if (point1.MysystemPunktId != point2.MysystemPunktId)
                {
                    await AddSwitching(point1.MysystemPunktId, point2.MysystemPunktId, cancellationToken);
                }
            }
        }


        //foreach (var newPoint in newPoints)
        //{
        //    if (baseLicence.MysystemPunktId != newPoint.MysystemPunktId)
        //    {
        //        await AddSwitching(baseLicence.MysystemPunktId, newPoint.MysystemPunktId, cancellationToken);
        //        await AddSwitching(newPoint.MysystemPunktId, baseLicence.MysystemPunktId, cancellationToken);
        //    }
        //}

        // Zapisujemy zmiany w bazie
        await _autoStacjaDbContext.SaveChangesAsync(cancellationToken);
        result.AssignedCount = newPoints.Count;
        return result;
    }

    // Metoda do pobrania informacji o salonie dla danego dealera na podstawie ID
    private async Task<SalonInfo> GetSalonInfoByDealerId(int dealerId, CancellationToken cancellationToken)
    {
        var salonInfo = await _discounts2AS.SalonInfos
            .Where(si => si.DealerNo == dealerId)  // Używamy DealerId
            .FirstOrDefaultAsync(cancellationToken);

        return salonInfo; // Zwracamy cały obiekt salonInfo
    }
    private async Task AddSwitching(long inId, long outId, CancellationToken cancellationToken)
    {
        var exists = await _autoStacjaDbContext.MysystemPunktCons
            .AnyAsync(c => c.MysystemPunktInId == inId && c.MysystemPunktOutId == outId, cancellationToken);

        if (!exists)
        {
            _autoStacjaDbContext.MysystemPunktCons.Add(new MysystemPunktCon
            {
                MysystemPunktInId = inId,
                MysystemPunktOutId = outId
            });
        }
    }
}