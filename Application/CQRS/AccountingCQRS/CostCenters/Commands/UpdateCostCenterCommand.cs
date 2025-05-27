using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.CostCenters.Commands
{
    public class UpdateCostCenterCommand(CostCenterVm costCenter) : IRequest<int>
    {
        public CostCenterVm CostCenter { get; set; } = costCenter;
    }
    public class UpdateCostCenterCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<UpdateCostCenterCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(UpdateCostCenterCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.CostCenters.FirstOrDefaultAsync(p => p.Id == request.CostCenter.Id, cancellationToken);
            _mapper.Map(request.CostCenter, item);
            await _context.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}
