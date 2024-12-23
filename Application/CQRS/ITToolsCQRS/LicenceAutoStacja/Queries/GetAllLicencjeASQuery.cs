using Application.Interfaces;
using Application.ViewModels.AutoStacja;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.CQRS.ITToolsCQRS.LicenceAutoStacja.Queries
{
    public class GetAllLicencjeASQuery : IRequest<IQueryable<MysystemPunktVm>>
    {
    }
    public class GetAllLicencjeASQueryHandler(IAutoStacjaDbContext autostacjaDbContext, IMapper mapper) : IRequestHandler<GetAllLicencjeASQuery, IQueryable<MysystemPunktVm>>
    {
        private readonly IAutoStacjaDbContext _autostacjaDbContext = autostacjaDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<MysystemPunktVm>> Handle(GetAllLicencjeASQuery request, CancellationToken cancellationToken)
        {
            var licencje = await _autostacjaDbContext.MysystemPunkts.Where(c => c.MysystemPunktId > 1)
                                               .AsNoTracking()
                                               .ToListAsync(cancellationToken);

            var licencjeAS = _mapper.Map<List<MysystemPunktVm>>(licencje);
            return licencjeAS.AsQueryable();
        }
    }
}
