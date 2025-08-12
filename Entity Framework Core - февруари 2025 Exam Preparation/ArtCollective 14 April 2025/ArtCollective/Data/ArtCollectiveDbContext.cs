using System.Reflection.PortableExecutable;
using ArtCollective.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtCollective.Data
{
    public class ArtCollectiveDbContext : DbContext
    {
        private const string ConnectionString = @"Server=.;Database=ArtCollectiveDb;Trusted_Connection=True;TrustServerCertificate=True";






        public ArtCollectiveDbContext()
        {

        }
        public ArtCollectiveDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ArtistGroup>()
                .HasKey(x => new { x.ArtistId, x.GroupId });








            modelBuilder.Entity<Collaboration>() //композитен ключ
                .HasKey(f => new {
                    f.ArtistOneId,
                    f.ArtistTwoId
                });

            

            //релация за едната линия
            modelBuilder.Entity<Collaboration>()
                .HasOne(f => f.ArtistOne)
                .WithMany(u => u.Collaboration1)
                .HasForeignKey(f => f.ArtistOneId)
                .OnDelete(DeleteBehavior.Restrict); //важно иначе се чупи

            //релация за другата линия
            modelBuilder.Entity<Collaboration>()
                .HasOne(f => f.ArtistTwo)
                .WithMany(u => u.Collaboration2)
                .HasForeignKey(f => f.ArtistTwoId)
                .OnDelete(DeleteBehavior.Restrict); //важно иначе се чупи





            modelBuilder.Entity<Artist>().HasData(
                new Artist { Id = 1, Username = "alessandro_rossi", Email = "alessandro.rossi@example.it", Password = "Rosso!Art24" },
                new Artist { Id = 2, Username = "marie_dupont", Email = "marie.dupont@example.fr", Password = "Dup0nt*Paris" },
                new Artist { Id = 3, Username = "giulia_bianchi", Email = "giulia.bianchi@example.it", Password = "Bianchi_1985" },
                new Artist { Id = 4, Username = "antoine_leblanc", Email = "antoine.leblanc@example.fr", Password = "Leblanc#3x" },
                new Artist { Id = 5, Username = "luca_verdi", Email = "luca.verdi@example.it", Password = "VerdiPass92!" },
                new Artist { Id = 6, Username = "camille_durand", Email = "camille.durand@example.fr", Password = "Durand$88" },
                new Artist { Id = 7, Username = "sofia_russo", Email = "sofia.russo@example.it", Password = "Sofia.R123" },
                new Artist { Id = 8, Username = "nicolas_moreau", Email = "nicolas.moreau@example.fr", Password = "Moreau_@Art" },
                new Artist { Id = 9, Username = "chiara_ferrari", Email = "chiara.ferrari@example.it", Password = "Chiara!Museo" },
                new Artist { Id = 10, Username = "élise_martin", Email = "elise.martin@example.fr", Password = "EliseM@rtin7" }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Title = "Impressionisti Moderni", StartedOn = DateTime.Parse("2024-01-15") },
                new Group { Id = 2, Title = "Art Contemporain", StartedOn = DateTime.Parse("2024-02-10") },
                new Group { Id = 3, Title = "Rinascimento Digitale", StartedOn = DateTime.Parse("2024-03-05") },
                new Group { Id = 4, Title = "Les Couleurs Libres", StartedOn = DateTime.Parse("2024-04-20") },
                new Group { Id = 5, Title = "Visioni Urbane", StartedOn = DateTime.Parse("2024-05-01") },
                new Group { Id = 6, Title = "Expression Visuelle", StartedOn = DateTime.Parse("2024-05-18") },
                new Group { Id = 7, Title = "Arte e Natura", StartedOn = DateTime.Parse("2024-06-10") },
                new Group { Id = 8, Title = "Surréalisme Moderne", StartedOn = DateTime.Parse("2024-06-30") }
            );

            modelBuilder.Entity<ArtistGroup>().HasData(
                new ArtistGroup { ArtistId = 1, GroupId = 1 },
                new ArtistGroup { ArtistId = 2, GroupId = 1 },
                new ArtistGroup { ArtistId = 4, GroupId = 1 },
                new ArtistGroup { ArtistId = 8, GroupId = 1 },
                new ArtistGroup { ArtistId = 9, GroupId = 1 },
                new ArtistGroup { ArtistId = 10, GroupId = 1 },

                new ArtistGroup { ArtistId = 1, GroupId = 2 },
                new ArtistGroup { ArtistId = 2, GroupId = 2 },
                new ArtistGroup { ArtistId = 3, GroupId = 2 },
                new ArtistGroup { ArtistId = 4, GroupId = 2 },
                new ArtistGroup { ArtistId = 5, GroupId = 2 },
                new ArtistGroup { ArtistId = 6, GroupId = 2 },
                new ArtistGroup { ArtistId = 7, GroupId = 2 },

                new ArtistGroup { ArtistId = 2, GroupId = 3 },
                new ArtistGroup { ArtistId = 4, GroupId = 3 },
                new ArtistGroup { ArtistId = 6, GroupId = 3 },

                new ArtistGroup { ArtistId = 3, GroupId = 4 },
                new ArtistGroup { ArtistId = 8, GroupId = 4 },
                new ArtistGroup { ArtistId = 9, GroupId = 4 },

                new ArtistGroup { ArtistId = 8, GroupId = 5 },
                new ArtistGroup { ArtistId = 4, GroupId = 5 },

                new ArtistGroup { ArtistId = 5, GroupId = 6 },
                new ArtistGroup { ArtistId = 6, GroupId = 6 },
                new ArtistGroup { ArtistId = 7, GroupId = 6 },

                new ArtistGroup { ArtistId = 6, GroupId = 7 },
                new ArtistGroup { ArtistId = 10, GroupId = 7 },

                new ArtistGroup { ArtistId = 7, GroupId = 8 },
                new ArtistGroup { ArtistId = 2, GroupId = 8 }
            );

            modelBuilder.Entity<Collaboration>().HasData(
                new Collaboration { ArtistOneId = 1, ArtistTwoId = 2 },
                new Collaboration { ArtistOneId = 1, ArtistTwoId = 3 },
                new Collaboration { ArtistOneId = 2, ArtistTwoId = 4 },
                new Collaboration { ArtistOneId = 2, ArtistTwoId = 5 },
                new Collaboration { ArtistOneId = 3, ArtistTwoId = 6 },
                new Collaboration { ArtistOneId = 4, ArtistTwoId = 7 },
                new Collaboration { ArtistOneId = 5, ArtistTwoId = 6 },
                new Collaboration { ArtistOneId = 6, ArtistTwoId = 9 },
                new Collaboration { ArtistOneId = 7, ArtistTwoId = 10 },
                new Collaboration { ArtistOneId = 8, ArtistTwoId = 9 },
                new Collaboration { ArtistOneId = 8, ArtistTwoId = 10 },
                new Collaboration { ArtistOneId = 3, ArtistTwoId = 9 }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<ArtistGroup> ArtistsGroups { get; set; } = null!;
        public DbSet<Artwork> Artworks { get; set; } = null!;
        public DbSet<Collaboration> Collaborations { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
    }
}
