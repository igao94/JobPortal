using Application.Admin.DTOs;
using Application.Core;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admin.GetUserWithRoles;

public class GetUserWithRolesHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUserWithRolesQuery, Result<List<UserWithRolesDto>>>
{
    public async Task<Result<List<UserWithRolesDto>>> Handle(GetUserWithRolesQuery request, 
        CancellationToken cancellationToken)
    {
        var usersWithRolesQuery = unitOfWork.AdminRepository.GetUsersQueryForRoles();

        var users = await usersWithRolesQuery.Select(u => new UserWithRolesDto
        {
            Id = u.Id,
            Username = u.UserName!,
            Roles = u.UserRoles.Select(r => r.Role.Name).ToList()!
        }).ToListAsync();

        return Result<List<UserWithRolesDto>>.Success(users);
    }
}
