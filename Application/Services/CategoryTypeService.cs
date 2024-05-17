using Application.Interfaces.ITWarehouse.Services;
using Application.ITWarehouseCQRS.CategoryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;
public class CategoryTypeService : ICategoryTypeService
{
    CategoryTypeVm ICategoryTypeService.AddCategoryType(CategoryTypeVm categoryTypeVm)
    {
        throw new NotImplementedException();
    }

    int ICategoryTypeService.DeleteCategoryType(int id)
    {
        throw new NotImplementedException();
    }

    List<CategoryTypeVm> ICategoryTypeService.GetAllCategoryTypes()
    {
        throw new NotImplementedException();
    }

    CategoryTypeVm ICategoryTypeService.GetCategoryTypeById()
    {
        throw new NotImplementedException();
    }

    CategoryTypeVm ICategoryTypeService.UpdateCategoryType(CategoryTypeVm categoryTypeVm)
    {
        throw new NotImplementedException();
    }
}
