using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class DeleteInvoiceItemCommandHandler : IRequestHandler<DeleteInvoiceItemCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    public DeleteInvoiceItemCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Handle(DeleteInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.InvoiceItems.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.InvoiceItems.Remove(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}
