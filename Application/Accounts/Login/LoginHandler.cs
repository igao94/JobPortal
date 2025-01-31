using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Login;

public class LoginHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<LoginCommand, Result<AccountDto>?>
{
    public async Task<Result<AccountDto>?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountsRepository.GetUserWithPhotosByEmailAsync(request.Email);

        if (user is null || user.UserName is null || user.Email is null) return null;

        var result = await unitOfWork.AccountsRepository.CheckPasswordAsync(user, request.Password);

        if (!result) return Result<AccountDto>.Failure("Invalid email or password.");

        return Result<AccountDto>.Success(new AccountDto
        {
            Name = user.Name,
            Username = user.UserName,
            Email = user.Email,
            Token = await tokenService.GetTokenAsync(user),
            Image = user.Photos.FirstOrDefault(p => p.IsMain)?.Url,
            ResumePath = null
        });
    }
}
