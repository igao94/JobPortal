using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsersRepository
{
    Task<AppUser?> GetUserByUsernameAsync(string username);
    IQueryable<AppUser> GetAllUsersQuery(string currentUserUsername);
    void DeleteUser(AppUser user);
    Task<AppUser?> GetUserWithPhotosByUsernameAsync(string username);
}
