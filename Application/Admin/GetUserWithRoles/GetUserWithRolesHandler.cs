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
        var users = unitOfWork.UsersRepository.GetUsersQueryForRoles();

        var userWithRoles = await users.Select(u =>
            new UserWithRolesDto(u.Id, u.UserName!, u.UserRoles.Select(r => r.Role.Name).ToList()!))
                .ToListAsync();

        return Result<List<UserWithRolesDto>>.Success(userWithRoles);
    }
}
