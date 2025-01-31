using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string? ResumePath { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
    public ICollection<JobApplication> JobApplications { get; set; } = [];
    public ICollection<Photo> Photos { get; set; } = [];
}
