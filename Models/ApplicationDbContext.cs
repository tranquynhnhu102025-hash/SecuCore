using Microsoft.EntityFrameworkCore;

namespace SecuCore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<WebsiteTarget> WebsiteTargets { get; set; }
    }
}