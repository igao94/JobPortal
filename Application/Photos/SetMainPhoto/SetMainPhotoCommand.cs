using Application.Core;
using MediatR;

namespace Application.Photos.SetMainPhoto;

public record SetMainPhotoCommand(string PhotoId) : IRequest<Result<Unit>>;

