namespace Domain.Entities;

public class JobApplication
{
    public Guid JobId { get; set; }
    public Job Job { get; set; } = null!;
    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";
}
