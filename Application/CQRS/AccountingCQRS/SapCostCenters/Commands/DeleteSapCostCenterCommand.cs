
using Application.Interfaces;
using Application.ViewModels.Accounting;

using AutoMapper;

using Domain.Entities.Accounting;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.SapCostCenters.Commands;
public class DeleteSapCostCenterCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
public class DeleteSapCostCenterCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteSapCostCenterCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<int> Handle(DeleteSapCostCenterCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.SapCostCenters
            .Where(p => p.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new KeyNotFoundException($"CostCenter with Id {request.Id} not found.");

        _appDbContext.SapCostCenters.Remove(result);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return result.Id;
    }
}