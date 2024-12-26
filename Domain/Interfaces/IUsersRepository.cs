using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsersRepository
{
    Task<AppUser?> GetUserWithPhotosAsync(string username);
    IQueryable<AppUser> GetUsersQueryForRoles();
    Task<AppUser?> GetUserByUsernameAsync(string username);
    void DeleteUser(AppUser user);
    void DeleteUserPhotos(AppUser user);
    IQueryable<AppUser> GetAllUsersQuery(string currentUsername, string? searchTerm);
}
