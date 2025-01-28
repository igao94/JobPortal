using Application.Core;
using Application.Jobs.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Jobs.CreateJob;

public class CreateJobHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateJobCommand, Result<JobDto>>
{
    public async Task<Result<JobDto>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = mapper.Map<Job>(request);

        unitOfWork.JobsRepository.AddJob(job);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<JobDto>.Success(mapper.Map<JobDto>(job))
            : Result<JobDto>.Failure("Failed to create a job.");
    }
}
