using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces;

public interface IPhotoService
{
    Task<PhotoUploadResult> UploadPhotoAsync(IFormFile file);
    void DeletePhoto(string url);
}
