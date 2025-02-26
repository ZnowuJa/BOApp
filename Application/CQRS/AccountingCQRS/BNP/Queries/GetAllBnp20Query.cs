using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.CQRS.AccountingCQRS.BNP.Queries
{
    public class GetAllBnp20Query(DateOnly startDate, DateOnly endDate) : IRequest<IQueryable<Bnp20Vm>>
    {
        //public string Bnp { get; set; }
        public DateOnly StartDate { get; set; } = startDate;
        public DateOnly EndDate { get; set; } = endDate;
    }

    public class GetAllBnp20QueryHandler(IAppDbContext appDbContext, IMapper mapper)
        : IRequestHandler<GetAllBnp20Query, IQueryable<Bnp20Vm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<Bnp20Vm>> Handle(GetAllBnp20Query request, CancellationToken cancellationToken)
        {
            var bnp20s = await _appDbContext.Bnp20s.Where(i => i.Data >= request.StartDate && i.Data <= request.EndDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var bnp20Vms = _mapper.Map<List<Bnp20Vm>>(bnp20s);
            return bnp20Vms.AsQueryable();
        }
    }
}