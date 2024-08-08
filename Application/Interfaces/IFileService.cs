using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Graph.Models;

namespace Application.Interfaces;
public interface IFileService
{
    Task MoveTemporaryFilesToPermanentLocationAsync(string sessionId, string sourceFolder,  string folderName, string numberPrefix, int id);
    Task<string> UploadFileAsync(IBrowserFile file, string folderName, string fileNumber);
    Task<string> UploadTemporaryFileAsync(IBrowserFile file, string sessionId);
}
