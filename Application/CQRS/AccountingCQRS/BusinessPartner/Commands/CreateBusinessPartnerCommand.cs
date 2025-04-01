using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.BusinessPartner.Commands
{
/*    public class CreateBusinessPartnerCommand(BusinessPartnerVm businessPartner) : IRequest<int>
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
            businessPartner.StatusId = 1;

            _context.BusinessPartners.Add(businessPartner);
            await _context.SaveChangesAsync(cancellationToken);

            return businessPartner.Id;
        }
    }*/
}
