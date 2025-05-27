using Application.ITWarehouseCQRS.CategoryTypes;
using Application.Mappings;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Categories;
public class CategoryDTO : IMapFrom<Category>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Prefix { get; set; }
    public CategoryTypeVm CategoryType { get; set; }
    public int StatusId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryDTO>();
    }
}
