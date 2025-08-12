using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data;

public class SalesContext : DbContext
{
    public SalesContext()
    {

    }

    public SalesContext(DbContextOptions<SalesContext> options)
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

        //  modelBuilder.ApplyConfiguration(new StudentConfiguration()); СЕЙВ ОПШЪНС ЗЗА FLUENT API
        //  modelBuilder.ApplyConfiguration(new AddressConfiguration());
        // modelBuilder.ApplyConfiguration(new RoomConfiguration());
        // modelBuilder.ApplyConfiguration(new StudentsCoursesConfiguration());


        base.OnModelCreating(modelBuilder);
    }
    //
    //  public DbSet<Student> Students { get; set; }  Колекции от таблици ентитита
    // public DbSet<Course> Courses { get; set; }
    // public DbSet<Room> Rooms { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Sale> Sales { get; set; }
    public DbSet<Store> Stores { get; set; }



}