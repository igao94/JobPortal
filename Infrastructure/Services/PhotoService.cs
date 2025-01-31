using Application.Interfaces;
using Application.Photos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class PhotoService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) : IPhotoService
{
    public async Task<PhotoUploadResult> UploadPhotoAsync(IFormFile file)
    {
        if (file is null || file.Length == 0)
            throw new ArgumentException("Upload a photo.");

        string[] validExtensions = [".jpg", ".jpeg", ".png"];

        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!validExtensions.Contains(extension))
            throw new ArgumentException($"Valid extensions are: {string.Join(", ", validExtensions)}.");

        if (file.Length > 5242880)
            throw new ArgumentException("Max size can be 5MB.");

        var folderPath = Path.Combine(env.WebRootPath, "Uploads", "Photos");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var photoId = Guid.NewGuid().ToString();

        var uniquePhotoName = $"{photoId}{extension}";

        var filePath = Path.Combine(folderPath, uniquePhotoName);

        using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream);

        var urlPhotoPath = $"{httpContextAccessor.HttpContext!.Request.Scheme}://" +
            $"{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}" +
            $"/Uploads/Photos/{uniquePhotoName}";

        return new PhotoUploadResult(photoId, urlPhotoPath);
    }

    public void DeletePhoto(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("Provide photo url.");

        var fileName = Path.GetFileName(new Uri(url).LocalPath);

        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("Invalid photo name.");

        var folderPath = Path.Combine(env.WebRootPath, "Uploads", "Photos");

        var filePath = Path.Combine(folderPath, fileName);

        if (!File.Exists(filePath)) throw new FileNotFoundException("Photo doesn't exist.");

        File.Delete(filePath);
    }
}
