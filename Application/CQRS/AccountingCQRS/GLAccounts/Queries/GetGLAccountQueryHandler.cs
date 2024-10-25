using Application.Interfaces;
using Application.ITWarehouseCQRS.States.Queries;
using Application.ViewModels;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Queries
{
    public class GetGLAccountQueryHandler : IRequestHandler<GetGLAccountQuery, GLAccountVm>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetGLAccountQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<GLAccountVm> Handle(GetGLAccountQuery request, CancellationToken cancellationToken)
        {
            var glAccount = await _appDbContext.GLAccounts
                                               .Where(a => a.Id == request.GLAccountId)
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<GLAccountVm>(glAccount);
        }
    }
}
