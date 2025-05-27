using Application.Mappings;
using AutoMapper;

using Domain.Common;

namespace Application.Common;
public class FormTemplateVm : IMapFrom<FormTemplate>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? TemplateArea { get; set; }
    public string? FolderName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FormTemplate, FormTemplateVm>().ReverseMap();
    }
}
