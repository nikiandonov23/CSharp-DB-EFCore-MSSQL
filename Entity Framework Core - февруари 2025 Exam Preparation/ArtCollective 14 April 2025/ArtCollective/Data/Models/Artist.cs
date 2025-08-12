using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtCollective.Data.Models;

public class Artist
{
    //	Id – integer, Primary Key
    //	Username – text with length[5, 30] (required)
    //	Email – text with length[6, 50] (required)
    //	Password – text with a minimum length of 4 (required)
    //	Artworks – a collection of type Artwork
    //	Feedbacks – a collection of type Feedback
    //	ArtistsGroups – a collection of type ArtistGroup

    [Key]
   public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    [MinLength(5)]
   public string Username { get; set; } = null!;


    [Required]
    [MaxLength(50)]
    [MinLength(6)]
   public string Email { get; set; } = null!;

    [Required]
    [MinLength(4)]
   public string Password { get; set; } = null!;



   public virtual ICollection<Artwork> Artworks { get; set; } = new HashSet<Artwork>();
   public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
   public virtual ICollection<ArtistGroup> ArtistsGroups { get; set; } = new HashSet<ArtistGroup>();



    [InverseProperty(nameof(Collaboration.ArtistOne))]
   public virtual ICollection<Collaboration> Collaboration1 { get; set; }=new HashSet<Collaboration>();




    [InverseProperty(nameof(Collaboration.ArtistTwo))]
    public virtual ICollection<Collaboration> Collaboration2 { get; set; } = new HashSet<Collaboration>();


    

}