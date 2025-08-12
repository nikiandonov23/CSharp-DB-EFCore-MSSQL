using System.ComponentModel.DataAnnotations;

namespace Boardgames.Data.Models;

public class Seller
{
    //	Country – text(required)
    //  	Website – a string (required). First four characters are "www.", followed by upper and lower letters, digits or '-' and the last three characters are ".com".
    //	BoardgamesSellers – collection of type BoardgameSeller
    [Key]
   public int Id { get; set; }



    [Required]
    [MaxLength(20)]
    [MinLength(5)]
   public string Name { get; set; } = null!;



    [Required]
    [MaxLength(30)]
    [MinLength(2)]
   public string Address { get; set; } = null!;



    [Required]
   public string Country { get; set; } = null!;


    [Required]
    [RegularExpression(@"^www\.[a-zA-Z0-9-]+\.com$")]
   public string Website { get; set; } = null!;
   public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; } = new List<BoardgameSeller>();
}