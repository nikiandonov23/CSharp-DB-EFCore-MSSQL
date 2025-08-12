using P02_FootballBettingCommon;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data;

using Microsoft.EntityFrameworkCore;

using static ApplicationCommonConfiguration;

public class FootballBettingContext : DbContext
{
    public FootballBettingContext()
    {

    }

    public FootballBettingContext(DbContextOptions<FootballBettingContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured == false)
        {
            optionsBuilder.UseSqlServer(ApplicationCommonConfiguration.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        //  modelBuilder.ApplyConfiguration(new StudentConfiguration()); СЕЙВ ОПШЪНС ЗЗА FLUENT API
        //  modelBuilder.ApplyConfiguration(new AddressConfiguration());
        // modelBuilder.ApplyConfiguration(new RoomConfiguration());
        // modelBuilder.ApplyConfiguration(new StudentsCoursesConfiguration());
        

        modelBuilder.Entity<PlayerStatistic>()
            .HasKey(ps => new { ps.GameId, ps.PlayerId });

        
    }

    //  public DbSet<Student> Students { get; set; }  Колекции от таблици ентитита
    // public DbSet<Course> Courses { get; set; }
    // public DbSet<Room> Rooms { get; set; }

    public DbSet<Bet> Bets { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerStatistic> PlayersStatistics { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Town> Towns { get; set; }
    public DbSet<User> Users { get; set; }

}