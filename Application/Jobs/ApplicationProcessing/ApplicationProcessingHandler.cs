using Application.Core;
using Domain.Interfaces;
using MediatR;
using System.Globalization;

namespace Application.Jobs.ApplicationProcessing;

public class ApplicationProcessingHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ApplicationProcessingCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(ApplicationProcessingCommand request,
        CancellationToken cancellationToken)
    {
        string[] validStatuses = ["Pending", "Accepted", "Rejected"];

        var normalizedRequestStatus = CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(request.Status.ToLower())
            .Trim();

        if (!validStatuses.Contains(normalizedRequestStatus))
            return Result<Unit>.Failure($"Choose one of valid statuses: {string.Join(", ", validStatuses)}.");

        var job = await unitOfWork.JobsRepository.GetJobByIdAsync(request.JobId);

        if (job is null) return null;

        var user = await unitOfWork.UsersRepository.GetUserByUsernameAsync(request.Username);

        if (user is null) return null;

        var jobApplication = await unitOfWork.JobsRepository.GetJobApplicationAsync(user.Id, job.Id);

        if (jobApplication is null) return null;

        jobApplication.Status = normalizedRequestStatus;

        var result = await unitOfWork.SaveAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update application.");
    }
}
