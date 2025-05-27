using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BusinessPartners.Queries
{
    public class GetAllBusinessPartnersByTypeNameAndLocationQuery(string typeName, string location) : IRequest<IQueryable<BusinessPartnerVm>>
    {
        public string TypeName { get; set; } = typeName;
        public string Location { get; set; } = location;
    }

    public class GetAllBusinessPartnersByTypeNameAndLocationQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllBusinessPartnersByTypeNameAndLocationQuery, IQueryable<BusinessPartnerVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<BusinessPartnerVm>> Handle(GetAllBusinessPartnersByTypeNameAndLocationQuery request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.BusinessPartners
                .Where(p => p.StatusId == 1 &&
                            p.BusinessPartnerType == request.TypeName &&
                            p.Location == request.Location)
                .ToListAsync(cancellationToken);

            var res = _mapper.Map<List<BusinessPartnerVm>>(result);

            return res.AsQueryable();
        }
    }
}