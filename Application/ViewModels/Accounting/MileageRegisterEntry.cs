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

    public DateTime? DateTimeForBinding
    {
        get => Date?.ToDateTime(TimeOnly.MinValue);
        set => Date = value.HasValue ? DateOnly.FromDateTime(value.Value) : null;
    }
    public decimal? TotalValue { get; set; }
    
    public int? Mileage { get; set; }
    public string? RouteDescription { get; set; }
    public string? Purpose { get; set; }
}

