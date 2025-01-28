using Application.Core;
using Application.Jobs.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Jobs.GetAllJobs;

public class GetAllJobsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllJobsQuery, Result<List<JobDto>>>
{
    public async Task<Result<List<JobDto>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
    {
        var jobs = await unitOfWork.JobsRepository.GetAllJobsAsync();

        return Result<List<JobDto>>.Success(mapper.Map<List<JobDto>>(jobs));
    }
}
