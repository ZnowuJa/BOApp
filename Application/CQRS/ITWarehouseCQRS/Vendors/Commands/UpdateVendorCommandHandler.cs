using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Vendors.Commands;
public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, int>
{
    private readonly IAppDbContext _context;

    public UpdateVendorCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var item = await _context.Vendors.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        item.Name = request.Name;
        await _context.SaveChangesAsync();
        return item.Id;

    }
}