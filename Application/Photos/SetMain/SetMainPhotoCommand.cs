using Application.Core;
using MediatR;

namespace Application.Photos.SetMain;

public record SetMainPhotoCommand(string PhotoId) : IRequest<Result<Unit>>;