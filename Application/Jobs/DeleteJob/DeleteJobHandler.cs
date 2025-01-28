using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Jobs.DeleteJob;

public class DeleteJobHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteJobCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var job = await unitOfWork.JobsRepository.GetJobByIdAsync(request.Id);

        if (job is null) return null;

        unitOfWork.JobsRepository.DeleteJob(job);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete a job.");
    }
}
