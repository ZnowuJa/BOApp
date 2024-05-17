using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Categories.Commands;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var ct = await _appDbContext.CategoryTypes.Where(p => p.Id == request.CategoryTypeVm.Id).FirstOrDefaultAsync();

        Category category = new()
        {
            Name = request.Name,
            Prefix = request.Prefix,
            CategoryType = ct
            //_mapper.Map<CategoryType>(request.CategoryTypeVm)

        };
        _appDbContext.Categories.Add(category);
        await _appDbContext.SaveChangesAsync();
        return category.Id;
    }
}
