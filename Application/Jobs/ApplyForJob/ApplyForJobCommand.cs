using Application.Core;
using MediatR;

namespace Application.Jobs.ApplyForJob;

public record ApplyForJobCommand(Guid JobId) : IRequest<Result<Unit>>;
