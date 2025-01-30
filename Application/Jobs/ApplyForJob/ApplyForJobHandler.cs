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
        var user = await unitOfWork
            .UsersRepository.GetUserByUsernameAsync(userAccessor.GetCurrentUserUsername());

        if (user is null) return null;

        var job = await unitOfWork.JobsRepository.GetJobByIdAsync(request.JobId);

        if (job is null) return null;

        var existingApplication = await unitOfWork.JobsRepository.GetJobApplicationByIdAsync(user.Id, job.Id);

        if (existingApplication is not null)
            return Result<Unit>.Failure("You have already applied for this job.");

        var jobApplication = new JobApplication
        {
            Job = job,
            JobId = job.Id,
            AppUser = user,
            AppUserId = user.Id
        };

        unitOfWork.JobsRepository.AddJobApplication(jobApplication);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to apply for a job.");
    }
}
