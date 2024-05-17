using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetCategoryTypeQueryHandler : IRequestHandler<GetCategoryTypeQuery, CategoryTypeVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetCategoryTypeQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<CategoryTypeVm> Handle(GetCategoryTypeQuery request, CancellationToken cancellationToken)
    {
        var categoryType = await _appDbContext.CategoryTypes.Where(p => p.Id == request.CategoryTypeId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var categoryTypeVM = _mapper.Map<CategoryTypeVm>(categoryType);
        return categoryTypeVM;
    }
}
