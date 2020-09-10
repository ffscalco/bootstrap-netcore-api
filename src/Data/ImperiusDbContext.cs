using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ImperiusDbContext : IdentityDbContext
    {
        public ImperiusDbContext(DbContextOptions<ImperiusDbContext> options)
            : base(options)
        {
        }

        public DbSet<Athlete> Athletes { get; set; }
    }
}
