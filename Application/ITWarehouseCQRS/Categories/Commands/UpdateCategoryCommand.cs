using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Categories.Commands;
public class UpdateCategoryCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Prefix { get; set; }
    public CategoryTypeVm CategoryTypeVm { get; set; }
    public UpdateCategoryCommand(int id, string name, string prefix, CategoryTypeVm categoryType)
    {
        Id = id;
        Name = name;
        Prefix = prefix;
        CategoryTypeVm = categoryType;
    }


}
