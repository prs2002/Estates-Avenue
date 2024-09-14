namespace DotNetBackend.Models
{
    using Microsoft.EntityFrameworkCore;

    namespace Aug22CodeFirstDemo.Models
    {
        public class PropertyContext : DbContext
        {
            public PropertyContext(DbContextOptions<PropertyContext> options)
                : base(options)
            {
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("Server = PRS\\SQLEXPRESS;Database=REDatabase;Trusted_Connection = True;Encrypt=false");
                }

            }

            public virtual DbSet<Property> GetProperty { get; set; }
        }

    }

}
