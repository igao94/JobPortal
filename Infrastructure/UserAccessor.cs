using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure;

public class UserAccessor(IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    public string GetCurrentUserUsername() => 
        httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name)!;
}
