using MediatR;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Categories.Commands;
public class DeleteCategoryCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
public class DeleteCategoryCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteCategoryCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {

        var result = await _appDbContext.Categories.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Categories.Remove(result);
        await _appDbContext.SaveChangesAsync();

        return result.Id;
    }
}