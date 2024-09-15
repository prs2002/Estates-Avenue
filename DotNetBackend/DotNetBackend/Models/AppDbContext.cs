using Microsoft.EntityFrameworkCore;

namespace DotNetBackend.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Property> Property { get; set; }
    }
}