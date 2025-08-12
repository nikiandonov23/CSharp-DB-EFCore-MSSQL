using System.ComponentModel.DataAnnotations;

namespace ArtCollective.Data.Models;

public class Group
{
    //	Id – integer, Primary Key
    //	Title – text with length[3, 50] (required)
    //	StartedOn – DateTime(required)
    //  •	Feedbacks – a collection of type Feedback
    //	ArtistsGroups – a collection of type ArtistGroup

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Title { get; set; } = null!;



    [Required]
    public DateTime StartedOn { get; set; }





    public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    public virtual ICollection<ArtistGroup> ArtistsGroups { get; set; } = new HashSet<ArtistGroup>();
}