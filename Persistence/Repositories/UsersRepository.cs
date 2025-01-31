using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UsersRepository(DataContext context) : IUsersRepository
{
    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserName == username.ToLower().Trim());
    }

    public IQueryable<AppUser> GetAllUsersQuery(string currentUserUsername)
    {
        return context.Users.Where(u => u.UserName != currentUserUsername).AsQueryable();
    }

    public void DeleteUser(AppUser user) => context.Users.Remove(user);

    public async Task<AppUser?> GetUserWithPhotosByUsernameAsync(string username)
    {
        return await context.Users
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.UserName == username.ToLower().Trim());
    }
}
