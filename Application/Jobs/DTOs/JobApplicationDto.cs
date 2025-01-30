namespace Application.Jobs.DTOs;

public class JobApplicationDto
{
    public string Username { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string? Image { get; set; }
    public string? ResumePath { get; set; }
}
