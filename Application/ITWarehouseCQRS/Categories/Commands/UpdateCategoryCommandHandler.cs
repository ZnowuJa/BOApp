using Application.Interfaces;
using Application.ITWarehouseCQRS.CategoryTypes.Commands;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Categories.Commands;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var ct = await _appDbContext.CategoryTypes.Where(p => p.Id == request.CategoryTypeVm.Id).FirstOrDefaultAsync();

        var cat = await _appDbContext.Categories.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        cat.Prefix = request.Prefix;
        cat.Name = request.Name;
        cat.CategoryType = ct;

        //Category category = new()
        //{
        //    Name = request.Name,
        //    Prefix = request.Prefix,
        //    CategoryType = ct
        //    //_mapper.Map<CategoryType>(request.CategoryTypeVm)

        //};
        _appDbContext.Categories.Update(cat);
        
        await _appDbContext.SaveChangesAsync();
        return cat.Id;
    }
}
