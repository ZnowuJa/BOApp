using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.Settlement;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.SettlementCQRS.GLAccounts.Queries
{
    public class GetAllGLAccountQueryHandler : IRequestHandler<GetAllGLAccountQuery, IQueryable<GLAccountVm>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAllGLAccountQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IQueryable<GLAccountVm>> Handle(GetAllGLAccountQuery request, CancellationToken cancellationToken)
        {
            var glAccounts = await _appDbContext.GLAccounts
                                                .Where(ct => ct.StatusId == 1)
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);

            var glAccountVms = _mapper.Map<List<GLAccountVm>>(glAccounts);
            return glAccountVms.AsQueryable();
        }
    }
}
