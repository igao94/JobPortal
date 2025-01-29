﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppRole : IdentityRole
{
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
}
