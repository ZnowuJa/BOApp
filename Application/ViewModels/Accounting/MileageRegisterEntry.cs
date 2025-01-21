using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Accounting;
public class MileageRegisterEntry
{
    public int Id { get; set; }
    public DateOnly? Date { get; set; }
    public decimal? TotalValue { get; set; }
    
    public int? Mileage { get; set; }
    public string? RouteDescription { get; set; }
    public string? Purpose { get; set; }
}

