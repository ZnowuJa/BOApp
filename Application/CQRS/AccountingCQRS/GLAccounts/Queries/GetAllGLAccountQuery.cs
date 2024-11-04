using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Queries
{
    public class GetAllGLAccountQuery : IRequest<IQueryable<GLAccountVm>>
    {
    }

    public class GetAllGLAccountQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllGLAccountQuery, IQueryable<GLAccountVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

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
