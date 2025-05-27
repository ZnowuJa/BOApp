using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    public DeleteInvoiceCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Invoices.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Invoices.Remove(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}
