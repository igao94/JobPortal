using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Resumes.AddResume;

public class AddResumeHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor,
    IFileService fileService) : IRequestHandler<AddResumeCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(AddResumeCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UsersRepository
            .GetUserByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        if (!string.IsNullOrEmpty(user.ResumePath)) fileService.DeleteFile(user.ResumePath);

        var uploadResult = await fileService.UploadFileAsync(request.File);

        user.ResumePath = uploadResult;

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to upload a resume.");
    }
}
