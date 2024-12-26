using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUserRole : IdentityUserRole<string>
{
    public AppUser User { get; set; } = default!;
    public AppRole Role { get; set; } = default!;
}
