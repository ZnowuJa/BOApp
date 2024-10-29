using Application.Interfaces;
using Application.ViewModels.Accounting;
using Application.ViewModels.CoC;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.CostCenters.Commands
{
    public class CreateCostCenterCommand(CostCenterVm costCenter) : IRequest<int>
    {
        public CostCenterVm CostCenter { get; set; } = costCenter;
    }

    public class CreateCostCenterCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<CreateCostCenterCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private IMapper _mapper { get; } = mapper;

        public async Task<int> Handle(CreateCostCenterCommand request, CancellationToken cancellationToken)
        {
            var costCenter = _mapper.Map<CostCenter>(request.CostCenter);
            costCenter.StatusId = 1;

            _context.CostCenters.Add(costCenter);
            await _context.SaveChangesAsync(cancellationToken);

            return costCenter.Id;
        }
    }
}
