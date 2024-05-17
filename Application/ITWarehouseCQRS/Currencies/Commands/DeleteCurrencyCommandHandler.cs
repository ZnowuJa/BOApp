using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Currencies.Commands;
public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, int>
{
    private readonly IAppDbContext _context;

    public DeleteCurrencyCommandHandler(IAppDbContext context)
    {
        _context = context;
        Console.WriteLine("Byłem tu!");
    }

    public async Task<int> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {

        var ct = await _context.Currencies.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _context.Currencies.Remove(ct);
        await _context.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + ct.Id);
        return ct.Id;
    }
}

