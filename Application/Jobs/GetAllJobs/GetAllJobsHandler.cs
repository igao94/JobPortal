using Application.Core;
using Application.Jobs.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.GetAllJobs;

public class GetAllJobsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllJobsQuery, Result<List<JobDto>>>
{
    public async Task<Result<List<JobDto>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
    {
        var jobsQuery = unitOfWork.JobsRepository.GetAllJobsQuery();

        var jobs = await jobsQuery.ProjectTo<JobDto>(mapper.ConfigurationProvider).ToListAsync();

        return Result<List<JobDto>>.Success(mapper.Map<List<JobDto>>(jobs));
    }
}
