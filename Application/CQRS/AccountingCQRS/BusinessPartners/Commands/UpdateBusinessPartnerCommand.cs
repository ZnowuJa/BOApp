using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BusinessPartners.Commands
{
    public class UpdateBusinessPartnerCommand(BusinessPartnerVm businessPartner) : IRequest<int>
    {
        public BusinessPartnerVm BusinessPartner { get; set; } = businessPartner;
    }

    public class UpdateBusinessPartnerCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<UpdateBusinessPartnerCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(UpdateBusinessPartnerCommand request, CancellationToken cancellationToken)
        {
            var businessPartner = await _context.BusinessPartners.FirstOrDefaultAsync(b => b.Id == request.BusinessPartner.Id, cancellationToken);

            _mapper.Map(request.BusinessPartner, businessPartner);
            await _context.SaveChangesAsync(cancellationToken);
            return businessPartner.Id;
        }
    }
}