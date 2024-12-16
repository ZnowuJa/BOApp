using Microsoft.FluentUI.AspNetCore.Components;

namespace BOAppFluentUI.Components.Pages;

public class FilterColumn<T>
{
    public bool FirstRow { get; set; }
    public string Label { get; set; }
    public Func<T, string> Property { get; set; }
    public string Filter { get; set; } = string.Empty;
    public bool IsVisible { get; set; }
    public string Width { get; set; }
    public string CssClass { get; set; } = string.Empty;
    public bool IsDropdown { get; set; } = false; // New property
    public bool IsDefaultSortColumn { get; set; } = false; // Domyślnie nie jest kolumną sortowania
    public SortDirection InitialSortDirection { get; set; } = SortDirection.Auto;
    public List<string> DropdownValues { get; set; } = new List<string>();
    public List<string> SelectedValues { get; set; } = new List<string>();

}
