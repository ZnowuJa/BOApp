using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Domain.Entities.Accounting;

namespace Application.CQRS.AccountingCQRS.BusinessPartners.Commands
{
    public class CreateBusinessPartnerCommand(BusinessPartnerVm businessPartner) : IRequest<int>
    {
        public BusinessPartnerVm BusinessPartner { get; set; } = businessPartner;
    }

    public class CreateBusinessPartnerCommandHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<CreateBusinessPartnerCommand, int>
    {
        private readonly IAppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<int> Handle(CreateBusinessPartnerCommand request, CancellationToken cancellationToken)
        {
            var businessPartner = _mapper.Map<BusinessPartner>(request.BusinessPartner);
            //businessPartner.StatusId = 1;

            _context.BusinessPartners.Add(businessPartner);
            await _context.SaveChangesAsync(cancellationToken);

            return businessPartner.Id;
        }
    }
}