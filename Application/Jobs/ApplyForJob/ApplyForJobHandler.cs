using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Jobs.ApplyForJob;

public class ApplyForJobHandler(IUnitOfWork unitOfWork,
    IUserAccessor userAccessor) : IRequestHandler<ApplyForJobCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
    {
        var job = await unitOfWork.JobsRepository.GetJobByIdAsync(request.JobId);

        if (job is null) return null;

        var user = await unitOfWork.UsersRepository
            .GetUserByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var existingApplication = await unitOfWork.JobsRepository.GetJobApplicationAsync(user.Id, job.Id);

        if (existingApplication is not null)
            return Result<Unit>.Failure("You have already applied for this job.");

        var jobApplication = new JobApplication
        {
            AppUser = user,
            AppUserId = user.Id,
            Job = job,
            JobId = job.Id
        };

        unitOfWork.JobsRepository.AddJobApplication(jobApplication);

        var result = await unitOfWork.SaveAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to apply for job.");
    }
}
