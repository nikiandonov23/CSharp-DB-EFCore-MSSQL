using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data;

public class HospitalContext : DbContext
{
    public HospitalContext()
    {

    }

    public HospitalContext(DbContextOptions<HospitalContext> options)
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


        modelBuilder.Entity<PatientMedicament>()
            .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Diagnose> Diagnoses { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<PatientMedicament> PatientMedicaments { get; set; }

    public DbSet<Visitation> Visitations { get; set; }


    //  public DbSet<Student> Students { get; set; }  Колекции от таблици ентитита
    // public DbSet<Course> Courses { get; set; }
    // public DbSet<Room> Rooms { get; set; }

}