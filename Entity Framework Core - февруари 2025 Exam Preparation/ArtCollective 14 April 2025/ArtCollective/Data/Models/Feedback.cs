using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ArtCollective.Data.Models.Enum;

namespace ArtCollective.Data.Models;

public class Feedback
{
    //	Id – integer, Primary Key
    //	Content – text with length[3, 200] (required)
    //	GivenOn – DateTime(required)
    //   •	Status – enum Status(Pending = 0, Reviewed, Published, Rejected)(required)
    //	GroupId - integer, Foreign Key(required)
    //   •	Group - Group
    //	ArtistId - integer, Foreign Key(required)
    //   •	Artist – Artist

    [Key]
    public int Id { get; set; }



    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public string Content { get; set; } = null!;



    [Required]
    public DateTime GivenOn { get; set; }




    [Required]
    public Status Status { get; set; }







    [ForeignKey(nameof(Group))]
    [Required]
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;



    [ForeignKey(nameof(Artist))] 
    [Required]
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

}