using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper.QueryableExtensions;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class DeleteCategoryTypeCommandHandler : IRequestHandler<DeleteCategoryTypeCommand, int>
{
    private readonly IAppDbContext _context;
    
    public DeleteCategoryTypeCommandHandler(IAppDbContext context)
    {
        _context = context;
        Console.WriteLine("Byłem tu!");
    }

    public async Task<int> Handle(DeleteCategoryTypeCommand request, CancellationToken cancellationToken)
    {

        var ct = await _context.CategoryTypes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _context.CategoryTypes.Remove(ct);
        await _context.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + ct.Id);
        return ct.Id;
    }
}
