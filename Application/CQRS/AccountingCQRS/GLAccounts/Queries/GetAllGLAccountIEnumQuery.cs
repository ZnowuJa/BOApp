using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Queries
{
    public class GetAllGLAccountIEnumQuery : IRequest<IEnumerable<GLAccountVm>>
    {
    }

    public class GetAllGLAccountIEnumQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllGLAccountIEnumQuery, IEnumerable<GLAccountVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<GLAccountVm>> Handle(GetAllGLAccountIEnumQuery request, CancellationToken cancellationToken)
        {
            var glAccounts = await _appDbContext.GLAccounts
                                                .Where(ct => ct.StatusId == 1)
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);
            
            return _mapper.Map<List<GLAccountVm>>(glAccounts);;
        }
    }
}
