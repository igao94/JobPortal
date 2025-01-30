using Application.Core;
using MediatR;

namespace Application.Admin.DeleteUser;

public record DeleteUserQuery(string Username) : IRequest<Result<Unit>>;
