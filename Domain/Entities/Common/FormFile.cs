namespace Domain.Entities.Common;
public class FormFile
{
    public int Id { get; set; }
    public string TmpPath { get; set; }
    public string TmpFileName { get; set; }
    public string TmpFileExtension { get; set; }
    public string? DstPath {get; set;}
    public string? DstFileName { get; set; }
    public string FormPurpose{get; set;} = "default";
    public string Prefix { get; set; }
    public string FolderName { get; set; }
    public string FormClassName { get; set; }
    public int? FormId { get; set; }
    public int Order { get; set; }
    public bool Deleted { get; set; } = false;
    
    public string OriginalFileName { get; set; }
}
