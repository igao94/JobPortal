using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.EditRoles;

public class EditRolesHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditRolesCommand, Result<List<string>>?>
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

        var existingRoles = await unitOfWork.AdminRepository.GetExistingRolesAsync();

        var invalidRoles = selectedRoles.Except(existingRoles).ToList();

        if (invalidRoles.Count != 0)
            return Result<List<string>>.Failure($"Valid roles are: {string.Join(", ", existingRoles)}.");

        var user = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username);

        if (user is null) return null;

        var userRoles = await unitOfWork.AdminRepository.GetRolesForUserAsync(user);

        var rolesToAdd = selectedRoles.Except(userRoles).ToList();

        var addResult = await unitOfWork.AdminRepository.AddUserToRolesAsync(user, rolesToAdd);

        if (!addResult.Succeeded) return Result<List<string>>.Failure("Failed to add to roles.");

        var rolesToRemove = userRoles.Except(rolesToAdd).ToList();

        var removeResult = await unitOfWork.AdminRepository.RemoveUserFromRolesAsync(user, rolesToRemove);

        if (!removeResult.Succeeded) return Result<List<string>>.Failure("Failed to remove roles.");

        var updatedRoles = await unitOfWork.AdminRepository.GetRolesForUserAsync(user);

        return Result<List<string>>.Success(updatedRoles);
    }
}
