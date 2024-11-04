using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.VATRates.Commands
{
    public class UpdateVATRateCommand(VATRateVm vatRate) : IRequest<int>
    {
        public VATRateVm VATRate { get; set; } = vatRate;
    }

    public class UpdateVATRateCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<UpdateVATRateCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(UpdateVATRateCommand request, CancellationToken cancellationToken)
        {
            var vatRate = await _context.VATRates.FirstOrDefaultAsync(v => v.Id == request.VATRate.Id, cancellationToken);
            _mapper.Map(request.VATRate, vatRate);
            await _context.SaveChangesAsync(cancellationToken);

            return vatRate.Id;
        }
    }
}
