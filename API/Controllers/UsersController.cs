using Application.Users;
using Application.Users.DeleteUser;
using Application.Users.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] UsersParams usersParams)
    {
        return HandlePagedResult(await mediator.Send(new GetAllUsersQuery(usersParams)));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser()
    {
        return HandleResult(await mediator.Send(new DeleteUserCommand()));
    }
}
