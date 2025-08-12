using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtCollective.Data.Models;

public class ArtistGroup
{
    //	ArtistId – integer, Primary Key, Foreign Key(required)
    //  •	Artist – Artist
    //	GroupId – integer, Primary Key, Foreign Key(required)
    //  •	Group – Group

    [ForeignKey(nameof(Artist))]
    [Required]
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;





    [ForeignKey(nameof(Group))]
    [Required]
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;


}