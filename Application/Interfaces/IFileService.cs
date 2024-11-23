using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Graph.Models;

namespace Application.Interfaces;
public interface IFileService
{
    Task MoveTemporaryFilesToPermanentLocationAsync(string sessionId, string sourceFolder,  string folderName, string numberPrefix, int id);
    Task<string> UploadFileAsync(IBrowserFile file, string folderName, string fileNumber);
    Task<Dictionary<string, string>> UploadTemporaryFileAsync(Stream fileStream, string fileName, string sessionId);
    Task MoveFormFilesToDestinationAsync(string tmpPath, string tmpFileName, string tmpFileExtension, string prefix, string folderName, string formClassName, string formId, int fileCounter);

}
