using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Categories.Commands;
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
