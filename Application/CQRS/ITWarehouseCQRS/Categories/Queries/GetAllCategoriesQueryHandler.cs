using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Categories.Queries;
public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IQueryable<CategoryVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllCategoriesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<IQueryable<CategoryVm>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {


        var result = await _appDbContext.Categories.Where(p => p.StatusId == 1).Include(i => i.CategoryType).ToListAsync(cancellationToken);
        var res = _mapper.Map<List<CategoryVm>>(result);
        //Console.WriteLine("Handler result" + res.Count);
        return res.AsQueryable();
    }
    
}
