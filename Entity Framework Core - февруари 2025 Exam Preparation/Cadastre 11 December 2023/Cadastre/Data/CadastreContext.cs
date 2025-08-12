using Cadastre.Data.Models;

namespace Cadastre.Data
{
    using Microsoft.EntityFrameworkCore;
    public class CadastreContext : DbContext
    {
        public CadastreContext()
        {
            
        }

        public CadastreContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<PropertyCitizen>()
                .HasKey(p => new { p.PropertyId, p.CitizenId });


        }

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyCitizen> PropertiesCitizens { get; set; }
        
    
    }
}
