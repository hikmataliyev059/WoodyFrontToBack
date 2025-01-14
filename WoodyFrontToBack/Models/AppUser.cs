﻿using Microsoft.AspNetCore.Identity;

namespace WoodyFrontToBack.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
}
