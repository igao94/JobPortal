using Application.Core;
using MediatR;

namespace Application.Admin.EditRoles;

public record EditRolesCommand(string Username, string Roles) : IRequest<Result<List<string>>>;
