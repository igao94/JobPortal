using Application.Core;
using Application.Jobs.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.GetAllJobs;

public class GetAllJobsHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllJobsQuery, Result<PagedList<JobDto>>>
{
    public async Task<Result<PagedList<JobDto>>> Handle(GetAllJobsQuery request, 
        CancellationToken cancellationToken)
    {
        var jobsQuery = unitOfWork.JobsRepository.GetAllJobsQuery(request.JobsParams.SearchTerm,
        request.JobsParams.SortColumn,
        request.JobsParams.SortOrder);

        var jobs = await PagedList<JobDto>
            .CreateAsync(jobsQuery.ProjectTo<JobDto>(mapper.ConfigurationProvider),
            request.JobsParams.PageNumber,
            request.JobsParams.PageSize);

        return Result<PagedList<JobDto>>.Success(jobs);
    }
}
