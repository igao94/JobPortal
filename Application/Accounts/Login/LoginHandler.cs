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
        var user = await unitOfWork.AccountsRepository.GetUserByEmailWithPhotosAsync(request.Email);

        if (user is null || user.UserName is null || user.Email is null)
            return null;

        var result = await unitOfWork.AccountsRepository.CheckPasswordAsync(user, request.Password);

        if (!result) return Result<AccountDto>.Failure("Invalid email or password.");

        var token = await tokenService.GetTokenAsync(user);

        var photo = user.Photos.FirstOrDefault(p => p.IsMain)?.Url;

        return Result<AccountDto>
            .Success(new AccountDto(user.Name, user.UserName, user.Email, token, photo, user.ResumePath));
    }
}
