using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relations_LAB.Data.Models;


[Table("Rooms",Schema = "uni")]
public class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Number { get; set; }


    public IList<Student> Students { get; set; } = new List<Student>();

}