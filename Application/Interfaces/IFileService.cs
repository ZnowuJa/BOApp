using Application.Common;
using Application.Forms;

using Microsoft.AspNetCore.Components.Forms;

namespace Application.Interfaces;
public interface IFileService
{
    Task<string> UploadFileAsync(IBrowserFile file, string  folderName, string fileNumber);
}
