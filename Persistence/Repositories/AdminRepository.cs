using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AdminRepository(UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager) : IAdminRepository
{
    public IQueryable<AppUser> GetUsersQueryForRoles()
    {
        return userManager.Users
            .OrderBy(u => u.UserName)
            .AsQueryable();
    }

    public async Task<List<string>> GetExistingRolesAsync()
    {
        return await roleManager.Roles
            .Where(r => !string.IsNullOrEmpty(r.Name))
            .Select(r => r.Name!.ToLower())
            .ToListAsync();
    }

    public async Task<List<string>> GetRolesForUserAsync(AppUser user)
    {
        var roles = (await userManager.GetRolesAsync(user)).Select(r => r.ToLower());

        return roles.ToList();
    }

    public async Task<IdentityResult> AddUserToRolesAsync(AppUser user, List<string> roles)
    {
        return await userManager.AddToRolesAsync(user, roles);
    }

    public async Task<IdentityResult> RemoveUserFromRolesAsync(AppUser user, List<string> roles)
    {
        return await userManager.RemoveFromRolesAsync(user, roles);
    }
}
