using Application.Accounts.DTOs;
using Application.Core;
using MediatR;

namespace Application.Accounts.Register;

public record RegisterCommand(string Name,
    string Username,
    string Email,
    string Password) : IRequest<Result<AccountDto>>;
