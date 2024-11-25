using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Companies.Commands;
public class DeleteCompanyCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    public DeleteCompanyCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Companies.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Companies.Remove(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}