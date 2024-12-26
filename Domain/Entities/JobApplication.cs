namespace Domain.Entities;

public class JobApplication
{
    public Guid JobId { get; set; }
    public Job Job { get; set; } = default!;
    public string AppUserId { get; set; } = default!;
    public AppUser AppUser { get; set; } = default!;
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";
}
