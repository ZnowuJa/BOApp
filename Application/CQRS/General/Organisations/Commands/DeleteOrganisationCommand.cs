using Application.Interfaces;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Organisations.Commands;
public class DeleteOrganisationCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteOrganisationCommand(int id)
    {
        Id = id;
    }
}


public class DeleteOrganisationCommandHandler : IRequestHandler<DeleteOrganisationCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteOrganisationCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Organisations.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Organisations.Remove(item);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return item.Id;
    }
}