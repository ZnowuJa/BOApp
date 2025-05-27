using Microsoft.AspNetCore.Components.Forms;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

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

    public async Task<Dictionary<string, string>> UploadTemporaryFileAsync(Stream fileStream, string fileName, string sessionId)
    {
        var tempFolder = Path.Combine(_environment.WebRootPath, "temp", sessionId);
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        var fileExtension = Path.GetExtension(fileName);
        var customFileName = $"{timestamp}{fileExtension}";

        var filePath = Path.Combine(tempFolder, customFileName);
        await using var newFileStream = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(newFileStream);

        return new Dictionary<string, string>
        {
            { "TmpPath", filePath },
            { "TmpFileName", customFileName },
            { "TmpFileExtension", fileExtension },
            { "OriginalFileName", fileName.ToUpper() }
        };
    }
    
    public async Task<Dictionary<string, string>> MoveTempFileToDestFolderAsync(string tmpPath, string folderName, string numberPrefix, int id)
    {
        var permanentFolder = Path.Combine(_environment.WebRootPath, folderName);

        if (!Directory.Exists(permanentFolder))
        {
            Directory.CreateDirectory(permanentFolder);
        }

        var fileExtension = Path.GetExtension(tmpPath);
        var idFormatted = id.ToString("D8");
        var fileNameOnly = Path.GetFileNameWithoutExtension(tmpPath);
        var newFileName = $"{numberPrefix}{idFormatted}_{fileNameOnly}{fileExtension}";
        var newFilePath = Path.Combine(permanentFolder, newFileName);

        File.Move(tmpPath, newFilePath);

        var result = new Dictionary<string, string>
        {
            { "DstPath", newFilePath },
            { "DstFileName", newFileName }
        };

        // Optionally delete the temp folder if it's empty
        var tempFolder = Path.GetDirectoryName(tmpPath);
        if (tempFolder != null && Directory.GetFiles(tempFolder).Length == 0)
        {
            Directory.Delete(tempFolder, true);
        }

        return result;
    }

    public void DeleteFile(string path)
    {
        try
        {
            File.Delete(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    public async Task<Dictionary<string, string>> MoveTemporaryFilesToPermanentLocationAsync(string sessionId, string folderName, string numberPrefix, int id)
    {
        var result = new Dictionary<string, string>
        {
            // { "TmpPath", filePath },
            // { "TmpFileName", customFileName },
            // { "TmpFileExtension", fileExtension },
            // { "OriginalFileName", fileName.ToUpper() }
        };
        var tempFolder = Path.Combine(_environment.WebRootPath, "temp", sessionId);
        var permanentFolder = Path.Combine(_environment.WebRootPath, folderName);

        if (!Directory.Exists(permanentFolder))
        {
            Directory.CreateDirectory(permanentFolder);
        }

        var tempFiles = Directory.GetFiles(tempFolder);
        var idFormatted = id.ToString("D8");
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
        return result;
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
