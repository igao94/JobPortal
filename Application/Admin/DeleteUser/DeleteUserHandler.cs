using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.DeleteUser;

public class DeleteUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserQuery, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteUserQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username);

        if (user is null) return null;

        unitOfWork.UsersRepository.DeleteUser(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.");
    }
}
