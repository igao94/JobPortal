namespace Application.Admin.DTOs;

public class UserWithRolesDto
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
}
