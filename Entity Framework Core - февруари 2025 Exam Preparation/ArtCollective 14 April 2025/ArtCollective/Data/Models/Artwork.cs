using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtCollective.Data.Models;

public class Artwork
{
    //	Id – integer, Primary Key
    //	Title – text with length[3, 50] (required)
    //	Description – text with length[10, 300] (NOT required)
    //	CreatedOn – DateTime(required)
    //   •	ArtistId – integer, Foreign Key(required)
    //   •	Artist – Artist


    [Key]
    public int Id { get; set; }



    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Title { get; set; } = null!;



    [MaxLength(300)]
    [MinLength(10)]
    public string? Description { get; set; }



    [Required]
    public DateTime CreatedOn { get; set; }





    [ForeignKey(nameof(Artist))]
    [Required]
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

}