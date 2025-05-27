using Microsoft.AspNetCore.Components.Forms;

namespace Application.Interfaces;
public interface IFileService
{
    Task<Dictionary<string, string>> MoveTemporaryFilesToPermanentLocationAsync(string sessionId,   string folderName, string numberPrefix, int id);
    Task<string> UploadFileAsync(IBrowserFile file, string folderName, string fileNumber);
    Task<Dictionary<string, string>> UploadTemporaryFileAsync(Stream fileStream, string fileName, string sessionId);
    Task MoveFormFilesToDestinationAsync(string tmpPath, string tmpFileName, string tmpFileExtension, string prefix, string folderName, string formClassName, string formId, int fileCounter);

    Task<Dictionary<string, string>> MoveTempFileToDestFolderAsync(string tmpPath, string folderName, string numberPrefix, int id);
    void DeleteFile(string path);

}
