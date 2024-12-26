using Application.Core;
using MediatR;

namespace Application.Admin.DeleteUser;

public record DeleteUserCommand(string Username) : IRequest<Result<Unit>>;
