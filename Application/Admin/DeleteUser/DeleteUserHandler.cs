using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Admin.DeleteUser;

public class DeleteUserHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService) : IRequestHandler<DeleteUserCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository.GetUserWithPhotosByUsernameAsync(request.Username);

        if (user is null) return null;

        if (user.Photos.Count != 0)
        {
            foreach (var photo in user.Photos)
            {
                photoService.DeletePhoto(photo.Url);
            }

            unitOfWork.PhotosRepository.DeleteUserPhotos(user);
        }

        unitOfWork.UsersRepository.DeleteUser(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.");
    }
}
