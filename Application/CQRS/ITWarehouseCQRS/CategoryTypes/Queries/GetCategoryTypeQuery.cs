using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetCategoryTypeQuery(int i) : IRequest<CategoryTypeVm>
{
    public int CategoryTypeId { get; set; } = i;
}
public class GetCategoryTypeQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetCategoryTypeQuery, CategoryTypeVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public IMapper Mapper { get; }

    public async Task<CategoryTypeVm> Handle(GetCategoryTypeQuery request, CancellationToken cancellationToken)
    {
        var categoryType = await _appDbContext.CategoryTypes.Where(p => p.Id == request.CategoryTypeId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var categoryTypeVM = _mapper.Map<CategoryTypeVm>(categoryType);
        return categoryTypeVM;
    }
}