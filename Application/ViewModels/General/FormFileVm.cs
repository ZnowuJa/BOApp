using Application.Mappings;

using AutoMapper;

using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.General;
public class FormFileVm : IMapFrom<FormFile>
{
    public int Id { get; set; }
    public string TmpPath { get; set; }
    public string TmpFileName { get; set; }
    public string TmpFileExtension { get; set; }
    public string Prefix { get; set; }
    public string FolderName { get; set; }
    public string FormClassName { get; set; }
    public int FormId { get; set; }
    public int Order { get; set; }
    public bool Deleted { get; set; } = false;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FormFile, FormFileVm>().ReverseMap();
        
    }

}
