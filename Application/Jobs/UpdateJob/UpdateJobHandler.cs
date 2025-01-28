using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Jobs.UpdateJob;

public class UpdateJobHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateJobCommand, Result<Unit>?>
{
    public async Task<Result<Unit>?> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await unitOfWork.JobsRepository.GetJobByIdAsync(request.Id);

        if (job is null) return null;

        mapper.Map(request, job);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update a job.");
    }
}
