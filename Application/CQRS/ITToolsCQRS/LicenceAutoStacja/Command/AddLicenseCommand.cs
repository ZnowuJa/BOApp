using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITTools.LicenceAutoStacja;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Command;

public class AddLicenseCommand : IRequest<int>
{
    public string LicenseName { get; }
    public int Dealer { get; }
    public string ComputerName { get; }

    public AddLicenseCommand(string licenseName, int dealer, string computerName)
    {
        LicenseName = licenseName;
        Dealer = dealer;
        ComputerName = computerName;
    }
}
public class AddLicenseCommandHandler(IAutoStacjaDbContext autoStacjaDbContext, IDiscounts2AS discounts2AS, IMapper mapper) : IRequestHandler<AddLicenseCommand, int>
{
    private readonly IAutoStacjaDbContext _autoStacjaDbContext = autoStacjaDbContext;
    private readonly IDiscounts2AS _discounts2AS = discounts2AS;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(AddLicenseCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.LicenseName) || string.IsNullOrWhiteSpace(command.ComputerName))
        {
            throw new Exception("Niepowodzenie: brak wymaganych danych");
        }

        var existingLicense = await _autoStacjaDbContext.MysystemPunkts
            .FirstOrDefaultAsync(x => x.Nazwa == command.LicenseName, cancellationToken);

        if (existingLicense != null)
        {
            throw new Exception($"Licencja '{command.LicenseName}' już istnieje.");
        }

        var salonInfo = await _discounts2AS.SalonInfos
            .FirstOrDefaultAsync(x => x.DealerNo == command.Dealer, cancellationToken) ?? throw new Exception($"Dealer '{command.Dealer}' nie istnieje.");

        var newLicense = new MysystemPunkt()
        {
            JednostkaOrgId = salonInfo.JednostkaOrg,
            Nazwa = command.LicenseName,
            PunktTelefon = command.ComputerName
        };

        _autoStacjaDbContext.MysystemPunkts.Add(newLicense);
        await _autoStacjaDbContext.SaveChangesAsync(cancellationToken);

        return (int)newLicense.MysystemPunktId;
    }
}