using Application.DTOs;

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

}
