using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Vendors.Commands;
public class UpdateVendorCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UpdateVendorCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
public class UpdateVendorCommandHandler(IAppDbContext context) : IRequestHandler<UpdateVendorCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<int> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var item = await _context.Vendors.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        item.Name = request.Name;
        await _context.SaveChangesAsync();
        return item.Id;

    }
}