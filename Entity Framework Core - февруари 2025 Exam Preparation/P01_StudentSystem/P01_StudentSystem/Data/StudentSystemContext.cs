using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data;

public class StudentSystemContext : DbContext
{
    public StudentSystemContext()
    {

    }

    public StudentSystemContext(DbContextOptions<StudentSystemContext> options)
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



        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.CourseId, sc.StudentId });

    }

    //  public DbSet<Student> Students { get; set; }  Колекции от таблици ентитита
    // public DbSet<Course> Courses { get; set; }
    // public DbSet<Room> Rooms { get; set; }

    public DbSet<Student> Students { get; set; }
    public DbSet<Homework> Homeworks { get; set; }

    public DbSet<StudentCourse> StudentsCourses { get; set; }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Resource> Resources { get; set; }

}