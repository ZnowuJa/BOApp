using Application.Mappings;
using Domain.Entities.ITWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Categories;
public class CategoriesVm 
{
    public ICollection<CategoryVm> Cats { get; set;}
}
