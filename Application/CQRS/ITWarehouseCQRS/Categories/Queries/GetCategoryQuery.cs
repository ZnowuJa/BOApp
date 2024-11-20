using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Categories.Queries;
public class GetCategoryQuery(int i) : IRequest<CategoryVm>
{
    public int CategoryId { get; set; } = i;
}
public class GetCategoryQueryHandler(IAppDbContext appDbContext, IMapper mappper) : IRequestHandler<GetCategoryQuery, CategoryVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mappper = mappper;

    public async Task<CategoryVm> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Categories.Where(p => p.Id == request.CategoryId).Include(i => i.CategoryType).FirstOrDefaultAsync();
        var res = _mappper.Map<CategoryVm>(result);
        return res;
    }
    //public aasync Task<CategoryVm> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    //{
    //    var category = await _appDbContext.Categories.Where(p => p.Id == request.CategoryId).FirstOrDefaultAsync(cancellationToken);
    //    var categoryVm = _mapper.Map<CategoryVm>(category);
    //    return categoryVm;
    //}
}