using Application.Core;
using MediatR;

namespace Application.Jobs.ApplicationProcessing;

public record ApplicationProcessingCommand(Guid JobId, string Username, string Status) : IRequest<Result<Unit>>;
