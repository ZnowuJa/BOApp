using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.VATRates.Commands
{
    namespace Application.CQRS.AccountingCQRS.VATRates.Commands
    {
        public class CreateVATRateCommand : IRequest<int>
        {
            public VATRateVm VATRate { get; set; }

            public CreateVATRateCommand(VATRateVm vatRate)
            {
                VATRate = vatRate;
            }
        }

        public class CreateVATRateCommandHandler : IRequestHandler<CreateVATRateCommand, int>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public CreateVATRateCommandHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateVATRateCommand request, CancellationToken cancellationToken)
            {
                var vatRate = _mapper.Map<VATRate>(request.VATRate);
                vatRate.StatusId = 1;

                _context.VATRates.Add(vatRate);
                await _context.SaveChangesAsync(cancellationToken);

                return vatRate.Id;
            }
        }
    }
}
