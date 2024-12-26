using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UsersRepository(UserManager<AppUser> userManager, DataContext context) : IUsersRepository
{
    public async Task<AppUser?> GetUserWithPhotosAsync(string username)
    {
        return await userManager.Users
            .Include(u => u.Photos)
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public IQueryable<AppUser> GetUsersQueryForRoles()
    {
        return userManager.Users
            .OrderBy(u => u.UserName)
            .AsQueryable();
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public void DeleteUser(AppUser user) => context.Users.Remove(user);

    public void DeleteUserPhotos(AppUser user) => context.Photos.RemoveRange(user.Photos);

    public IQueryable<AppUser> GetAllUsersQuery(string currentUsername, string? searchTerm)
    {
        IQueryable<AppUser> query = context.Users.Where(u => u.UserName != currentUsername);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(u =>
                u.Name.Contains(searchTerm) ||
                u.UserName!.Contains(searchTerm) ||
                u.Email!.Contains(searchTerm));
        }

        return query;
    }
}
