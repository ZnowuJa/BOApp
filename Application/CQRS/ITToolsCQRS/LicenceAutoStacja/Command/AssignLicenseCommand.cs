using Application.Interfaces;
using Application.ViewModels.AutoStacja;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Command;

public class AssignLicenseCommand(string licenseName, string computerName) : IRequest<Unit>
{
    public string LicenseName { get; set; } = licenseName;
    public string ComputerName { get; set; } = computerName;
}

public class AssignLicenseCommandHandler(IAutoStacjaDbContext autoStacjaDbContext, IMapper mapper) : IRequestHandler<AssignLicenseCommand, Unit>
{
    private readonly IAutoStacjaDbContext _autoStacjaDbContext = autoStacjaDbContext;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Unit> Handle(AssignLicenseCommand request, CancellationToken cancellationToken)
    {
        var punkt = await _autoStacjaDbContext.MysystemPunkts
            .FirstOrDefaultAsync(p => p.Nazwa == request.LicenseName, cancellationToken);

        if (punkt == null)
        {
            throw new Exception($"Brak licencji: {request.LicenseName}");
        }

        punkt.PunktTelefon = request.ComputerName;
        
        await _autoStacjaDbContext.SaveChangesAsync(cancellationToken);

        // Map the updated entity to the ViewModel
        //var punktVm = _mapper.Map<MysystemPunktVm>(punkt);

        return Unit.Value;
    }
}