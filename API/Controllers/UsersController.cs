using Application.Users.DeleteUser;
using Application.Users.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return HandleResult(await mediator.Send(new GetAllUsersQuery()));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        return HandleResult(await mediator.Send(new DeleteUserCommand()));
    }
}
