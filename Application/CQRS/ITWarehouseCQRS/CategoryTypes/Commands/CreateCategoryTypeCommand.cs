using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.CategoryTypes.Commands;
public class CreateCategoryTypeCommand(string name) : IRequest<int>
{
    public string Name { get; set; } = name;
}
public class CreateCategoryTypeCommandHandler(IAppDbContext context) : IRequestHandler<CreateCategoryTypeCommand, int>
{
    private readonly IAppDbContext _context = context;

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