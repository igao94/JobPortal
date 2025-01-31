using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsersRepository
{
    Task<AppUser?> GetUserByUsernameAsync(string username);
    IQueryable<AppUser> GetAllUsersQuery(string currentUserUsername, string? searchTerm);
    void DeleteUser(AppUser user);
    Task<AppUser?> GetUserWithPhotosByUsernameAsync(string username);
}
