using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relations_LAB.Data.Configurations;
using Relations_LAB.Data.Models;

namespace Relations_LAB.Data;

public class UniversityDbContext : DbContext
{
    public UniversityDbContext()
    {

    }

    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
    : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured == false)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new StudentsCoursesConfiguration());


        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Room> Rooms { get; set; }

}