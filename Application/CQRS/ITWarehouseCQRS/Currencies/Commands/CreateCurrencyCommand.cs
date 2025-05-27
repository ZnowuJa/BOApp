using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Currencies.Commands;
public class CreateCurrencyCommand(string name) : IRequest<int>
{
    public string Name { get; set; } = name;
}
public class CreateCurrencyCommandHandler(IAppDbContext context) : IRequestHandler<CreateCurrencyCommand, int>
{
    private readonly IAppDbContext _context = context;

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
