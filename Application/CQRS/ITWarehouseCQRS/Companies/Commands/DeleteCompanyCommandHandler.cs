using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Companies.Commands;
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
