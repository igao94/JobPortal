namespace Application.Accounts.DTOs;

public record AccountDto(string Name, 
    string Username, 
    string Email, 
    string? Token, 
    string? Image,
    string? ResumePath);
