using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Currencies.Commands;
public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, int>
{
    private readonly IAppDbContext _context;

    public UpdateCurrencyCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var currency = await _context.Currencies.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        currency.Name = request.Name;
        await _context.SaveChangesAsync();
        return currency.Id;

    }
}