using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsersRepository
{
    Task<AppUser?> GetUserByUsernameAsync(string username);
}
