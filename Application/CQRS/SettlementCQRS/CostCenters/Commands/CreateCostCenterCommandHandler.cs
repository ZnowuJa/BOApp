using Application.Interfaces;
using Domain.Entities.Settlement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.CostCenters.Commands
{
    public class CreateCostCenterCommandHandler : IRequestHandler<CreateCostCenterCommand, int>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateCostCenterCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(CreateCostCenterCommand request, CancellationToken cancellationToken)
        {
            CostCenter costCenter = new()
            {
                MPK = request.MPK,
                Description = request.Description,
                Text = request.Text,
                StatusId = 1
            };

            _appDbContext.CostCenters.Add(costCenter);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return costCenter.Id;
        }
    }
}
