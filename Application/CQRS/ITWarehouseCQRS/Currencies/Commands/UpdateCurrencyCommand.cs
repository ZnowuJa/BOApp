using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Currencies.Commands;
public class UpdateCurrencyCommand(int id, string name) : IRequest<int>
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
}
public class UpdateCurrencyCommandHandler(IAppDbContext context) : IRequestHandler<UpdateCurrencyCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<int> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var currency = await _context.Currencies.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        currency.Name = request.Name;
        await _context.SaveChangesAsync();
        return currency.Id;

    }
}