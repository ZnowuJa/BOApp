using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class UpdateCategoryTypeCommandHandler : IRequestHandler<UpdateCategoryTypeCommand, int>
{
    private readonly IAppDbContext _context;

    public UpdateCategoryTypeCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateCategoryTypeCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var categoryType = await _context.CategoryTypes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        categoryType.Name = request.Name;
        await _context.SaveChangesAsync();
        return categoryType.Id;

    }
}
