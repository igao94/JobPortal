using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Persistence.Authorization.Constants;

namespace Application.Accounts.Register;

public class RegisterHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<RegisterCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var lowerCaseUsername = request.Username.ToLower().Trim();

        if (await unitOfWork.AccountsRepository.IsEmailTakenAsync(request.Email))
            return Result<AccountDto>.Failure("Email is already taken.");

        if (await unitOfWork.AccountsRepository.IsUsernameTakenAsync(lowerCaseUsername))
            return Result<AccountDto>.Failure("Username is already taken.");

        var user = new AppUser
        {
            Name = request.Username,
            UserName = lowerCaseUsername,
            Email = request.Email
        };

        var creationResult = await unitOfWork.AccountsRepository.CreateUserAsync(user, request.Password);

        if (!creationResult.Succeeded) return Result<AccountDto>.Failure("Unable to register user.");

        var roleResult = await unitOfWork.AccountsRepository.AddUserToRoleAsync(user, UserRoles.User);

        if (!roleResult.Succeeded) return Result<AccountDto>.Failure("Failed to add role to user.");

        return Result<AccountDto>.Success(new AccountDto
        {
            Name = user.Name,
            Username = user.UserName,
            Email = user.Email,
            Token = await tokenService.GetTokenAsync(user),
            Image = null,
            ResumePath = null,
        });
    }
}
