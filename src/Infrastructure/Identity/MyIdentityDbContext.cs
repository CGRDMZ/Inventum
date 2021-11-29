using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class MyIdentityDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public MyIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
