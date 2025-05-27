using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BusinessPartners.Queries
{
    public class GetAllBusinessPartnersByTypeNameQuery(string typeName) : IRequest<IQueryable<BusinessPartnerVm>>
    {
        public string TypeName { get; set; } = typeName;
    }

    public class GetAllBusinessPartnersByTypeNameQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllBusinessPartnersByTypeNameQuery, IQueryable<BusinessPartnerVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<BusinessPartnerVm>> Handle(GetAllBusinessPartnersByTypeNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.BusinessPartners
                .Where(p => p.StatusId == 1 && p.BankTransferType == request.TypeName)
                .ToListAsync(cancellationToken);

            var res = _mapper.Map<List<BusinessPartnerVm>>(result);

            return res.AsQueryable();
        }
    }
}