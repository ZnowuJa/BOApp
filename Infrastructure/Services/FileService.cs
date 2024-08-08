using Microsoft.AspNetCore.Components.Forms;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Application.Common;
using Domain.Entities.Common;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadFileAsync(IBrowserFile file, string folderName, string fileNumber)
    {
        var uploadPath = Path.Combine(_environment.WebRootPath, folderName);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        //NAZWA pliku: numer formularza, numer id formularza z autouzupełnianiem do 8 cyfr file.name NP. PZ20240611

        var fileName = Path.GetFileName(file.Name);
        var filePath = Path.Combine(uploadPath, fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.OpenReadStream().CopyToAsync(fileStream);
        return fileName;
    }

    public async Task<string> UploadTemporaryFileAsync(IBrowserFile file, string sessionId)
    {
        var tempFolder = Path.Combine(_environment.WebRootPath, "temp", sessionId);
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var fileExtension = Path.GetExtension(file.Name);
        var customFileName = $"{timestamp}{fileExtension}";

        var filePath = Path.Combine(tempFolder, customFileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.OpenReadStream().CopyToAsync(fileStream);
        return customFileName;
    }

    public async Task MoveTemporaryFilesToPermanentLocationAsync(string sessionId, string sourceFolder, string folderName, string numberPrefix, int id)
    {
        var tempFolder = Path.Combine(_environment.WebRootPath, "temp", sessionId);
        var permanentFolder = Path.Combine(_environment.WebRootPath, folderName);

        if (!Directory.Exists(permanentFolder))
        {
            Directory.CreateDirectory(permanentFolder);
        }

        var tempFiles = Directory.GetFiles(sourceFolder);
        var idFormatted = id.ToString("D10");
        var fileCounter = 1;

        foreach (var tempFile in tempFiles)
        {
            var fileExtension = Path.GetExtension(tempFile);
            var newFileName = $"{numberPrefix}{idFormatted}_{fileCounter:D2}{fileExtension}";
            var newFilePath = Path.Combine(permanentFolder, newFileName);

            File.Move(tempFile, newFilePath);
            fileCounter++;
        }

        // Optionally delete the temp folder after moving files
        Directory.Delete(tempFolder, true);
    }

    public async Task MoveFormFilesToDestinationAsync(string tmpPath, string tmpFileName,string tmpFileExtension, string prefix, string folderName, string formClassName, string formId, int fileCounter)
    {
        var sourceFilePath = Path.Combine(tmpPath, tmpFileName);
        var fid = int.Parse(formId);
        var idFormatted = fid.ToString("D10");
        var destFolder = Path.Combine(_environment.WebRootPath, folderName);

        if (!Directory.Exists(destFolder))
        {
            Directory.CreateDirectory(destFolder);
        }

        var newFileName = $"{prefix}{idFormatted}_{fileCounter:D2}{tmpFileExtension}";
        var newFilePath = Path.Combine(destFolder, newFileName);

        File.Move(sourceFilePath, newFilePath);

    }
}
