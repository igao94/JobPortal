using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces;

public interface IAdminRepository
{
    IQueryable<AppUser> GetUsersQueryForRoles();
    Task<List<string>> GetExistingRolesAsync();
    Task<List<string>> GetRolesForUserAsync(AppUser user);
    Task<IdentityResult> AddUserToRolesAsync(AppUser user, List<string> roles);
    Task<IdentityResult> RemoveUserFromRolesAsync(AppUser user, List<string> roles);
}
