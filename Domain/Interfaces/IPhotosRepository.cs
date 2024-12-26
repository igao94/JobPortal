using Domain.Entities;

namespace Domain.Interfaces;

public interface IPhotosRepository
{
    void DeletePhoto(Photo photo);
}
