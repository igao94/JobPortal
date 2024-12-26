namespace Application.Jobs.DTOs;

public class JobApplicationDto
{
    public string Username { get; set; } = default!;
    public string Status { get; set; } = "Pending";
    public string? Image { get; set; }
    public string? ResumePath { get; set; }
}