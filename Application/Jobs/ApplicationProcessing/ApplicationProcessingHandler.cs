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

        var lowerUsername = request.Username.ToLower().Trim();

        var normalizedRequestStatus = CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(request.Status.ToLower())
            .Trim();

        if (!validStatuses.Contains(normalizedRequestStatus))
            return Result<Unit>.Failure($"Choose one of valid statuses: {string.Join(", ", validStatuses)}.");

        var user = await unitOfWork.UsersRepository.GetUserByUsernameAsync(lowerUsername);

        if (user is null) return Result<Unit>.Failure("User doesn't exist.");

        var job = await unitOfWork.JobsRepository.GetJobByIdAsync(request.JobId);

        if (job is null) return Result<Unit>.Failure("Job doesn't exist.");

        var jobApplication = await unitOfWork.JobsRepository.GetJobApplicationByIdAsync(user.Id, job.Id);

        if (jobApplication is null) return Result<Unit>.Failure("Job application doesn't exist.");

        jobApplication.Status = normalizedRequestStatus;

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update application.");
    }
}
