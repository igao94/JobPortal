using Application.Core;
using Application.Jobs.DTOs;
using MediatR;

namespace Application.Jobs.GetAllJobs;

public record GetAllJobsQuery : IRequest<Result<List<JobDto>>>;
