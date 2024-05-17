using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetAllCategoryTypesQueryHandler : IRequestHandler<GetAllCategoryTypesQuery, IQueryable<CategoryTypeVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllCategoryTypesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCategoryTypesQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<CategoryTypeVm>> Handle(GetAllCategoryTypesQuery request, CancellationToken cancellationToken)
    {
        var cts =  await _appDbContext.CategoryTypes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var ctslist = _mapper.Map<List<CategoryTypeVm>>(cts);

        return ctslist.AsQueryable();
    }

}
