using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Queries
{
    public class GetGLAccountQuery(int i) : IRequest<GLAccountVm>
    {
        public int GLAccountId { get; set; } = i;
    }
    public class GetGLAccountQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetGLAccountQuery, GLAccountVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

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
