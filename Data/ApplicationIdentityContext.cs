﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace SportClubs1.Data
{
    public class ApplicationIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public
       ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options)
        : base(options)
        {
        }
    }


}
