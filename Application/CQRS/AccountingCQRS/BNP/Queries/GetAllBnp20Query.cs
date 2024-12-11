using Application.CQRS.AccountingCQRS.BNP.Queries;
using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using Domain.Entities.BNP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.BNP.Queries
{
    public class GetAllBnp20Query(DateOnly startDate, DateOnly endDate) : IRequest<IQueryable<Bnp20Vm>>
    {
        public DateOnly StartDate { get; set; } = startDate;
        public DateOnly EndDate { get; set; } = endDate;
    }
    public class GetAllBnp20QueryHandler(IBNPDbContext bnpDbContext, IMapper mapper) : IRequestHandler<GetAllBnp20Query, IQueryable<Bnp20Vm>>
    {
        private readonly IBNPDbContext _bnpDbContext = bnpDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<Bnp20Vm>> Handle(GetAllBnp20Query request, CancellationToken cancellationToken)
        {

            var bnp20s = await _bnpDbContext.Bnp20s.Where(i => i.Data >= request.StartDate && i.Data <= request.EndDate)
                                            .AsNoTracking()
                                            .ToListAsync(cancellationToken);

            var bnp20Vms = _mapper.Map<List<Bnp20Vm>>(bnp20s);
            return bnp20Vms.AsQueryable();
        }
    }
}
    