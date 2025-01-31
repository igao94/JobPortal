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

    public IQueryable<AppUser> GetAllUsersQuery(string currentUserUsername, string? searchTerm)
    {
        IQueryable<AppUser> query = context.Users.Where(u => u.UserName != currentUserUsername);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u => u.Name.Contains(searchTerm)
            || u.UserName!.Contains(searchTerm)
            || u.Email!.Contains(searchTerm));
        }

        return query;
    }

    public void DeleteUser(AppUser user) => context.Users.Remove(user);

    public async Task<AppUser?> GetUserWithPhotosByUsernameAsync(string username)
    {
        return await context.Users
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.UserName == username.ToLower().Trim());
    }
}
