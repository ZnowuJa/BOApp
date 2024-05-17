using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.States.Commands;
public class UpdateStateCommandHandler : IRequestHandler<UpdateStateCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateStateCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var item = await _appDbContext.States.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        item.Name = request.Name;
        item.Description = request.Description;
        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
