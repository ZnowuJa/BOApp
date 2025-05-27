using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.VATRates.Commands
{
    public class CreateVATRateCommand(VATRateVm vatRate) : IRequest<int>
    {
        public VATRateVm VATRate { get; set; } = vatRate;
    }

    public class CreateVATRateCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<CreateVATRateCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

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
