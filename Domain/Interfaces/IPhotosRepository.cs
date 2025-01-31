using Domain.Entities;

namespace Domain.Interfaces;

public interface IPhotosRepository
{
    void DeleteUserPhotos(AppUser user);
    void DeletePhoto(Photo photo);
}
