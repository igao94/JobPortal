using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Resumes;

public record AddResumeCommand(IFormFile File) : IRequest<Result<Unit>>;
