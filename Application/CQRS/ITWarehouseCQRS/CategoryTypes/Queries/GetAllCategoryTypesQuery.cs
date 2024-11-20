using MediatR;
using Application.ViewModels;
using Application.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetAllCategoryTypesQuery : IRequest<IQueryable<CategoryTypeVm>>
{
}
public class GetAllCategoryTypesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCategoryTypesQueryHandler> logger) : IRequestHandler<GetAllCategoryTypesQuery, IQueryable<CategoryTypeVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<CategoryTypeVm>> Handle(GetAllCategoryTypesQuery request, CancellationToken cancellationToken)
    {
        var cts = await _appDbContext.CategoryTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var ctslist = _mapper.Map<List<CategoryTypeVm>>(cts);

        return ctslist.AsQueryable();
    }

}