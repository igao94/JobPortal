namespace Application.Accounts.DTOs;

public class AccountDto
{
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Token { get; set; }
    public string? Image { get; set; }
    public string? ResumePath { get; set; }
}
