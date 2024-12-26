namespace Domain.Entities;

public class Job
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public string Location { get; set; } = default!;
    public DateTime PostedDate { get; set; } = DateTime.UtcNow;
    public ICollection<JobApplication> Applicants { get; set; } = [];
}
