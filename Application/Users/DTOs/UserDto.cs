using Application.Photos.DTOs;

namespace Application.Users.DTOs;

public class UserDto
{
    public string Name { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Image { get; set; }
    public string? ResumePath { get; set; }
    public ICollection<PhotoDto> Photos { get; set; } = [];
}
