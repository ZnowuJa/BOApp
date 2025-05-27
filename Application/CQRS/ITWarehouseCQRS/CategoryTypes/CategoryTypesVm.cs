using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CategoryTypes;
public class CategoryTypesVm
{
    public ICollection<CategoryTypeVM> CategoryTypes { get; set; }
}
