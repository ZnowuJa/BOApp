using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.BNP.Queries
{
    public class GetAllBnp55Query : IRequest<IQueryable<Bnp55Vm>>
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public GetAllBnp55Query(DateOnly startDate, DateOnly endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
    public class GetAllBnp55QueryHandler(IBNPDbContext bnpDbContext, IMapper mapper) : IRequestHandler<GetAllBnp55Query, IQueryable<Bnp55Vm>>
    {
        private readonly IBNPDbContext _bnpDbContext = bnpDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<Bnp55Vm>> Handle(GetAllBnp55Query request, CancellationToken cancellationToken)
        {
            var bnp55s = await _bnpDbContext.Bnp55s.Where(i => i.Data >= request.StartDate && i.Data <= request.EndDate)
                                            .AsNoTracking()
                                            .ToListAsync(cancellationToken);

            var bnp55Vms = _mapper.Map<List<Bnp55Vm>>(bnp55s);
            return bnp55Vms.AsQueryable();
        }
    }
}
