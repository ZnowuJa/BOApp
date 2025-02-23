using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.SapCostCenters.Commands;
public class UpdateSapCostCenterCommand(SapCostCenterVm costCenter) : IRequest<int>
{
    public SapCostCenterVm CostCenter { get; set; } = costCenter;
}
public class UpdateSapCostCenterCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<UpdateSapCostCenterCommand, int>
{
    private readonly IAppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(UpdateSapCostCenterCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.SapCostCenters.FirstOrDefaultAsync(p => p.Id == request.CostCenter.Id, cancellationToken);
        _mapper.Map<SapCostCenter>(request.CostCenter);
        await _context.SaveChangesAsync(cancellationToken);
        return item.Id;
    }
}
