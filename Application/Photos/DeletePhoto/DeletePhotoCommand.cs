using Application.Core;
using MediatR;

namespace Application.Photos.DeletePhoto;

public record DeletePhotoCommand(string PhotoId) : IRequest<Result<Unit>>;
