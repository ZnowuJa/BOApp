using Microsoft.AspNetCore.Components.Forms;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Application.Common;



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
}
