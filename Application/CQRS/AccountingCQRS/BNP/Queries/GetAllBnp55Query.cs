using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BNP.Queries
{
    public class GetAllBnp55Query(DateOnly startDate, DateOnly endDate) : IRequest<IQueryable<Bnp55Vm>>
    {
        public DateOnly StartDate { get; set; } = startDate;
        public DateOnly EndDate { get; set; } = endDate;
    }
    public class GetAllBnp55QueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllBnp55Query, IQueryable<Bnp55Vm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<Bnp55Vm>> Handle(GetAllBnp55Query request, CancellationToken cancellationToken)
        {
            var bnp55s = await _appDbContext.Bnp55s.Where(i => i.Data >= request.StartDate && i.Data <= request.EndDate)
                                            .AsNoTracking()
                                            .ToListAsync(cancellationToken);

            var bnp55Vms = _mapper.Map<List<Bnp55Vm>>(bnp55s);
            return bnp55Vms.AsQueryable();
        }
    }
}
