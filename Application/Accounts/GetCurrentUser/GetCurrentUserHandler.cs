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
            .GetUserWithPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null || user.UserName is null || user.Email is null) return null;

        return Result<AccountDto>.Success(new AccountDto
        {
            Name = user.Name,
            Username = user.UserName,
            Email = user.Email,
            Token = await tokenService.GetTokenAsync(user),
            Image = user.Photos.FirstOrDefault(p => p.IsMain)?.Url,
            ResumePath = user.ResumePath
        });
    }
}
