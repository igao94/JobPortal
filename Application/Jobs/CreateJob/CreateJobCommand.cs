using Application.Core;
using Application.Jobs.DTOs;
using MediatR;

namespace Application.Jobs.CreateJob;

public record CreateJobCommand(string Title,
    string Description,
    string CompanyName,
    string Location) : IRequest<Result<JobDto>>;
