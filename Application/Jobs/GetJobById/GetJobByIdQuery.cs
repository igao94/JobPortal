using Application.Core;
using Application.Jobs.DTOs;
using MediatR;

namespace Application.Jobs.GetJobById;

public record GetJobByIdQuery(Guid Id) : IRequest<Result<JobDto>>;
