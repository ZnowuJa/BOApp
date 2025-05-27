using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Categories.Commands;
public class CreateCategoryCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Prefix { get; set; }
    public int LeadingZeros { get; set; }
    public CategoryTypeVm CategoryTypeVm { get; set; }

    public CreateCategoryCommand(string name, string prefix, int leadingZeros, CategoryTypeVm categoryTypeVm)
    {
        Name = name;
        Prefix = prefix;
        CategoryTypeVm = categoryTypeVm;
        LeadingZeros = leadingZeros;
    }
}
public class CreateCategoryCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var ct = await _appDbContext.CategoryTypes.Where(p => p.Id == request.CategoryTypeVm.Id).FirstOrDefaultAsync();

        Category category = new()
        {
            Name = request.Name,
            Prefix = request.Prefix,
            LeadingZeros = request.LeadingZeros,
            CategoryType = ct
            //_mapper.Map<CategoryType>(request.CategoryTypeVm)

        };
        _appDbContext.Categories.Add(category);
        await _appDbContext.SaveChangesAsync();
        return category.Id;
    }
}