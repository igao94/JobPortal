using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.EditRoles;

public class EditRolesHandler(UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager) : IRequestHandler<EditRolesCommand, Result<List<string>>?>
{
    public async Task<Result<List<string>>?> Handle(EditRolesCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Roles))
            return Result<List<string>>.Failure("You must select at least one role.");

        var selectedRoles = request.Roles
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.Trim().ToLower())
            .Distinct();

        var existingRoles = await roleManager.Roles
            .Where(r => !string.IsNullOrEmpty(r.Name))
            .Select(r => r.Name!.ToLower())
            .ToListAsync();

        var invalidRoles = selectedRoles.Except(existingRoles).ToList();

        if (invalidRoles.Count != 0)
            return Result<List<string>>.Failure($"Valid roles: {string.Join(", ", existingRoles)}");

        var user = await userManager.FindByNameAsync(request.Username);

        if (user is null) return null;

        var userRoles = (await userManager.GetRolesAsync(user))
            .Select(r => r.ToLower());

        var rolesToAdd = selectedRoles.Except(userRoles);

        var addResult = await userManager.AddToRolesAsync(user, rolesToAdd);

        if (!addResult.Succeeded) return Result<List<string>>.Failure("Failed to add roles.");

        var rolesToRemove = userRoles.Except(selectedRoles);

        var removeResult = await userManager.RemoveFromRolesAsync(user, rolesToRemove);

        if (!removeResult.Succeeded) return Result<List<string>>.Failure("Failed to remove roles.");

        var updatedRoles = await userManager.GetRolesAsync(user);

        return Result<List<string>>.Success(updatedRoles.ToList());
    }
}
