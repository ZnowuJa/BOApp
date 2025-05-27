using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Categories.Commands;
public class UpdateCategoryCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Prefix { get; set; }
    public int LeadingZeros { get; set; }
    public CategoryTypeVm CategoryTypeVm { get; set; }
    public UpdateCategoryCommand(int id, string name, string prefix, int leadingZeros, CategoryTypeVm categoryType)
    {
        Id = id;
        Name = name;
        Prefix = prefix;
        CategoryTypeVm = categoryType;
        LeadingZeros = leadingZeros;
    }
}
public class UpdateCategoryCommandHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<UpdateCategoryCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var ct = await _appDbContext.CategoryTypes.Where(p => p.Id == request.CategoryTypeVm.Id).FirstOrDefaultAsync();

        var cat = await _appDbContext.Categories.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        cat.Prefix = request.Prefix;
        cat.Name = request.Name;
        cat.CategoryType = ct;
        cat.LeadingZeros = request.LeadingZeros;

        //Category category = new()
        //{
        //    Title = request.Title,
        //    Prefix = request.Prefix,
        //    CategoryType = ct
        //    //_mapper.Map<CategoryType>(request.CategoryTypeVm)

        //};
        _appDbContext.Categories.Update(cat);

        await _appDbContext.SaveChangesAsync();
        return cat.Id;
    }
}