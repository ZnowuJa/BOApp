using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.AAA_IDEAS;
public class filter_collection_by_multiple_properties
{

    //ItemForm listItem = new ItemForm();
    //public filter_collection_by_multiple_properties()
    //{
    //    var listItem = new ItemForm();        
    //}

    public void Filter()
    {
        
        //var collection = GetCollection();
        //if (collection == null)
        //if (dto.filterbyname not null or empty)
        //    {
        //    collection = collection.Where(x.name == name)
        //     };
        //if (dto.filterbycountry not null or empty)
        //    {
        //    collection = collection.Where(x.country == country)
        //        };
    }
    
}
public class Item
{
    public Item(int id, string name, string type, string description, string country, string phone)
    {
        Id = id;
        Name = name;
        Type = type;
        Description = description;
        Country = country;
        Phone = phone;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
}

public class ItemForm
{
    List<Item> items = new();

    public ItemForm(List<Item> items, string filterByName, string filterByType, string filterByCountry)
    {
        this.items = items;
        this.filterByName = filterByName;
        this.filterByType = filterByType;
        this.filterByCountry = filterByCountry;
    }

    public string filterByName { get; set; }
    public string filterByType { get; set; }
    public string filterByCountry { get; set; }

}
