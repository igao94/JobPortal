using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Resumes.AddResume;

public record AddResumeCommand(IFormFile File) : IRequest<Result<Unit>>;
