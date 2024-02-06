using Microsoft.EntityFrameworkCore;

namespace LocationAvailabilityAPI.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, constraints, etc.
        }
    }
}
