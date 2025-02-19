using Application.CQRS.AccountingCQRS.BusinessTravels.Commands;
using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Commands
{
    public class UpdateAdvancePaymentCommand(AdvancePaymentFormVm item) : IRequest<AdvancePaymentFormVm>
    {
        public AdvancePaymentFormVm Item { get; set; } = item;
    }

    public class UpdateAdvancePaymentCommandHandler(IAppDbContext appDbContext, IMapper mapper, IConfiguration configuration) : IRequestHandler<UpdateAdvancePaymentCommand, AdvancePaymentFormVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;
        public async Task<AdvancePaymentFormVm> Handle(UpdateAdvancePaymentCommand request, CancellationToken cancellationToken)
        {
            var employee = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.EnovaEmpId)).FirstOrDefaultAsync(cancellationToken);
            var manager = await _appDbContext.Employees.Where(p => p.EnovaEmpId == int.Parse(request.Item.LVL1_EnovaEmpId)).FirstOrDefaultAsync(cancellationToken);

            using var transaction = await _appDbContext.BeginTransactionAsync();
            try
            {
                var item = await _appDbContext.AdvancePayments.FindAsync(request.Item.Id, cancellationToken);
                if (item != null)
                {
                    _mapper.Map(request.Item, item);
                }
                _appDbContext.AdvancePayments.Update(item);
                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
            return request.Item;
        }
    }
}