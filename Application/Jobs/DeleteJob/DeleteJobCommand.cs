using Application.Core;
using MediatR;

namespace Application.Jobs.DeleteJob;

public record DeleteJobCommand(Guid Id) : IRequest<Result<Unit>>;
