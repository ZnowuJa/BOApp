using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Categories.Commands;
public class CreateCategoryCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Prefix { get; set; }
    public CategoryTypeVm CategoryTypeVm { get; set; }

    public CreateCategoryCommand(string name, string prefix, CategoryTypeVm categoryTypeVm)
    {
        Name = name;
        Prefix = prefix;
        CategoryTypeVm = categoryTypeVm;
    }



 

}
