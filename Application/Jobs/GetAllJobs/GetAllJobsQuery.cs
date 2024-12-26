using Application.Core;
using Application.Jobs.DTOs;
using MediatR;

namespace Application.Jobs.GetAllJobs;

public record GetAllJobsQuery(JobsParams JobsParams) : IRequest<Result<PagedList<JobDto>>>;
