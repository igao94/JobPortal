using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file);
    string DeleteFile(string url);
}
