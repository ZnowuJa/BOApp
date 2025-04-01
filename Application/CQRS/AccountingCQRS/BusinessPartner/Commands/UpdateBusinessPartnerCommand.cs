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
/*    public class UpdateBusinessPartnerCommand(BusinessPartnerVm businessPartner) : IRequest<int>
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
    }*/
}
