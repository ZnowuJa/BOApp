using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Vendors.Commands;
public class DeleteVendorCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
public class DeleteVendorCommandHandler(IAppDbContext context) : IRequestHandler<DeleteVendorCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<int> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {

        var item = await _context.Vendors.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _context.Vendors.Remove(item);
        await _context.SaveChangesAsync();

        return item.Id;
    }
}