using Application.Admin.DTOs;
using Application.Core;
using MediatR;

namespace Application.Admin.GetUserWithRoles;

public record GetUserWithRolesQuery : IRequest<Result<List<UserWithRolesDto>>>;
