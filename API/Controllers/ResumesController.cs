using Application.Resumes.AddResume;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ResumesController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> UploadResume([FromForm] AddResumeCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }
}
