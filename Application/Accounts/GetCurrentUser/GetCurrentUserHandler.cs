using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.GetCurrentUser;

public class GetCurrentUserHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    ITokenService tokenService) : IRequestHandler<GetCurrentUserQuery, Result<AccountDto>?>
{
    public async Task<Result<AccountDto>?> Handle(GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosAsync(userAccessor.GetCurrentUserUsername());

        if (user is null || user.UserName is null || user.Email is null) return null;

        var token = await tokenService.GetTokenAsync(user);

        var photo = user.Photos.FirstOrDefault(p => p.IsMain)?.Url;

        return Result<AccountDto>
            .Success(new AccountDto(user.Name, user.UserName, user.Email, token, photo, user.ResumePath));
    }
}
