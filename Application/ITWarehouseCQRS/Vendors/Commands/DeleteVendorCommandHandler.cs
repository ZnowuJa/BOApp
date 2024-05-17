using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Vendors.Commands;
public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, int>
{
    private readonly IAppDbContext _context;

    public DeleteVendorCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {

        var item = await _context.Vendors.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _context.Vendors.Remove(item);
        await _context.SaveChangesAsync();

        return item.Id;
    }
}
