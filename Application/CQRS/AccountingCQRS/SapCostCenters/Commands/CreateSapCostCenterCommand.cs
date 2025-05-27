using Application.Interfaces;
using Application.ViewModels.Accounting;

using AutoMapper;

using Domain.Entities.Accounting;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.SapCostCenters.Commands;
public class CreateSapCostCenterCommand(SapCostCenterVm costCenter) : IRequest<int>
{
    public SapCostCenterVm CostCenter { get; set; } = costCenter;
}

public class CreateSapCostCenterCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<CreateSapCostCenterCommand, int>
{
    private readonly IAppDbContext _context = context;
    private IMapper _mapper { get; } = mapper;

    public async Task<int> Handle(CreateSapCostCenterCommand request, CancellationToken cancellationToken)
    {
        var costCenter = _mapper.Map<SapCostCenter>(request.CostCenter);
        costCenter.StatusId = 1;

        _context.SapCostCenters.Add(costCenter);
        await _context.SaveChangesAsync(cancellationToken);

        return costCenter.Id;
    }
}
