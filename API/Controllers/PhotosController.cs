using Application.Photos.AddPhoto;
using Application.Photos.DeletePhoto;
using Application.Photos.SetMainPhoto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace API.Controllers;

public class PhotosController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> AddPhoto([FromForm] AddPhotoCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }

    [HttpPut("{photoId}/setMainPhoto")]
    public async Task<IActionResult> SetMainPhoto(string photoId)
    {
        return HandleResult(await mediator.Send(new SetMainPhotoCommand(photoId)));
    }

    [HttpDelete("{photoId}")]
    public async Task<IActionResult> DeletePhoto(string photoId)
    {
        return HandleResult(await mediator.Send(new DeletePhotoCommand(photoId)));
    }
}
