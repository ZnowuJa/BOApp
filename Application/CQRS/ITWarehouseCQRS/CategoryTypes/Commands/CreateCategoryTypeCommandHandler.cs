using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class CreateCategoryTypeCommandHandler : IRequestHandler<CreateCategoryTypeCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateCategoryTypeCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryTypeCommand request, CancellationToken cancellationToken)
    {
        CategoryType categoryType = new()
        {
            Name = request.Name,
            StatusId = 1
        };
        _context.CategoryTypes.Add(categoryType);
        await _context.SaveChangesAsync();

        return categoryType.Id;
    }
}
