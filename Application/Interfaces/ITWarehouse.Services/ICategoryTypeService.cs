using Application.ITWarehouseCQRS.CategoryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITWarehouse.Services;
public interface ICategoryTypeService
{
    List<CategoryTypeVm> GetAllCategoryTypes();
    CategoryTypeVm GetCategoryTypeById();
    CategoryTypeVm AddCategoryType(CategoryTypeVm categoryTypeVm);
    CategoryTypeVm UpdateCategoryType(CategoryTypeVm categoryTypeVm);
    int DeleteCategoryType(int id);

}
