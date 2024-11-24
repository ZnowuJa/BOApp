using Application.Interfaces;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.GroupCoCs.Commands;
public class DeleteGroupCoCCommand : IRequest<int>
{
    public int Id { get; set; }

    public DeleteGroupCoCCommand(int id)
    {
        Id = id;
    }
}

public class DeleteGroupCoCCommandHandler : IRequestHandler<DeleteGroupCoCCommand, int>
{
    private readonly IAppDbContext _dbContext;

    public DeleteGroupCoCCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(DeleteGroupCoCCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

        //if (entity == null)
        //    throw new NotFoundException(nameof(GroupCoC), request.Id);

        _dbContext.Groups.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request.Id;
    }
}
