﻿using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Photos.DeletePhoto;

public class DeletePhotoHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IPhotoService photoService) : IRequestHandler<DeletePhotoCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserWithPhotosByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var photo = user.Photos.FirstOrDefault(p => p.PhotoId == request.PhotoId);

        if (photo is null) return null;

        if (photo.IsMain) return Result<Unit>.Failure("Can't delete main photo.");

        photoService.DeletePhoto(photo.Url);

        unitOfWork.PhotosRepository.DeletePhoto(photo);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete photo.");
    }
}
