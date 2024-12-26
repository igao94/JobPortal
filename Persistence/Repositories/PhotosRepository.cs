using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.Repositories;

public class PhotosRepository(DataContext context) : IPhotosRepository
{
    public void DeletePhoto(Photo photo) => context.Photos.Remove(photo);
}
