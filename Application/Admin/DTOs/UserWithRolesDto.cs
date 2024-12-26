namespace Application.Admin.DTOs;

public record UserWithRolesDto(string Id, string Username, List<string> Roles);
