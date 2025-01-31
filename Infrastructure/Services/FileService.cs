using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FileService(IWebHostEnvironment env,
    IHttpContextAccessor httpContextAccessor) : IFileService
{
    public async Task<string> UploadFileAsync(IFormFile file)
    {
        if (file is null || file.Length == 0) throw new ArgumentException("Upload a resume.");

        string[] validExtensions = [".pdf", ".txt", ".docx"];

        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!validExtensions.Contains(extension))
            throw new ArgumentException($"Valid extensions are: {string.Join(", ", validExtensions)}.");

        if (file.Length > 5242880) throw new InvalidOperationException("Max size can be 5MB.");

        var folderPath = Path.Combine(env.WebRootPath, "Uploads", "Files");

        if(!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        var fileId = Guid.NewGuid().ToString();

        var uniqueFileName = $"{fileId}{extension}";

        var filePath = Path.Combine(folderPath, uniqueFileName);

        using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream);

        var urlFilePath = $"{httpContextAccessor.HttpContext!.Request.Scheme}://" +
            $"{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}" +
            $"/Uploads/Files/{uniqueFileName}";

        return urlFilePath;
    }

    public void DeleteFile(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url), "Provide file url.");

        var fileName = Path.GetFileName(new Uri(url).LocalPath);

        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("Invalid file name.");

        var folderPath = Path.Combine(env.WebRootPath, "Uploads", "Files");

        var filePath = Path.Combine(folderPath , fileName);

        if (!File.Exists(filePath)) throw new FileNotFoundException("The file doesn't exist");

        File.Delete(filePath);
    }
}
