using Application.Admin.DeleteUser;
using Application.Admin.EditRoles;
using Application.Admin.GetUserWithRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Authorization.Constants;

namespace API.Controllers;

public class AdminController(IMediator mediator) : BaseApiController
{
    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpGet("users-with-roles")]
    public async Task<IActionResult> GetUsersWithRoles()
    {
        return HandleResult(await mediator.Send(new GetUserWithRolesQuery()));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteUser(string username)
    {
        return HandleResult(await mediator.Send(new DeleteUserCommand(username)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpGet("edit-roles/{username}")]
    public async Task<IActionResult> EditRoles(string username, string roles)
    {
        return HandleResult(await mediator.Send(new EditRolesCommand(username, roles)));
    }
}
