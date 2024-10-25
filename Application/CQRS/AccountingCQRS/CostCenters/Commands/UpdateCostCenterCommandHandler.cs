using Application.Interfaces;
using Application.ITWarehouseCQRS.Invoices.Commands;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.CostCenters.Commands
{
    public class UpdateCostCenterCommandHandler : IRequestHandler<UpdateCostCenterCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public UpdateCostCenterCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateCostCenterCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.CostCenters.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

            _mapper.Map(request, result);

            _appDbContext.CostCenters.Update(result);
            await _appDbContext.SaveChangesAsync();
            return result.Id;
        }
    }
}
