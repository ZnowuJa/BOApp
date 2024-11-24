using Application.Interfaces;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.Positions.Commands;
public class DeletePositionCommand : IRequest<int>
{
    public int Id { get; set; }

    public DeletePositionCommand(int id)
    {
        Id = id;
    }
}

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, int>
{
    private readonly IAppDbContext _dbContext;

    public DeletePositionCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Positions.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        //if (entity == null)
        //    throw new NotFoundException(nameof(Position), request.Id);

        _dbContext.Positions.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}
