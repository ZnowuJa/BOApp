using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Currencies.Commands;
public class DeleteCurrencyCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
public class DeleteCurrencyCommandHandler(IAppDbContext context) : IRequestHandler<DeleteCurrencyCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<int> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {

        var ct = await _context.Currencies.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _context.Currencies.Remove(ct);
        await _context.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + ct.Id);
        return ct.Id;
    }
}
