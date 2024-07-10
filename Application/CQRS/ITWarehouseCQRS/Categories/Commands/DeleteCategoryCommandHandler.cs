using Application.Interfaces;
using Application.ITWarehouseCQRS.CategoryTypes.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Categories.Commands;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteCategoryCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

    }

    public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {

        var result = await _appDbContext.Categories.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Categories.Remove(result);
        await _appDbContext.SaveChangesAsync();

        return result.Id;
    }
}
