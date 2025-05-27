using Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITWarehouse.Services;
public interface ICategoryService
{
    List<CategoryVm> GetAllCategories();
    CategoryVm GetCategoryById();
    CategoryVm AddCategory(CategoryVm categoryVm);
    CategoryVm UpdateCategory(CategoryVm categoryVm);
    int DeleteCategory(int id);
}
