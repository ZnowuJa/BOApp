using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Categories.Queries;
public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
 

    public GetCategoryQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }

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

