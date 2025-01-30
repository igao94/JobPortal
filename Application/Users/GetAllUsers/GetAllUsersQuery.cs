using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.GetAllUsers;

public class GetAllUsersQuery : IRequest<Result<List<UserDto>>>;
