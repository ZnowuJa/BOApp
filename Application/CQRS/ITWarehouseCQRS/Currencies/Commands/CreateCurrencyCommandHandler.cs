using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.Currencies.Commands;
public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateCurrencyCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        Currency currency = new()
        {
            Name = request.Name,
            StatusId = 1
        };
        _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();

        return currency.Id;
    }
}

