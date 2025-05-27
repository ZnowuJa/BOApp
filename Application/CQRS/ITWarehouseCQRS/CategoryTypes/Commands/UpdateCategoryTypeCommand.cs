using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.CategoryTypes.Commands;
public class UpdateCategoryTypeCommand(int id, string name) : IRequest<int>
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
}
public class UpdateCategoryTypeCommandHandler(IAppDbContext context) : IRequestHandler<UpdateCategoryTypeCommand, int>
{
    private readonly IAppDbContext _context = context;

    public async Task<int> Handle(UpdateCategoryTypeCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var categoryType = await _context.CategoryTypes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        categoryType.Name = request.Name;
        await _context.SaveChangesAsync();
        return categoryType.Id;
    }
}