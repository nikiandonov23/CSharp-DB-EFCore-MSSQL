using System.ComponentModel.DataAnnotations;

namespace Trucks.Data.Models;

public class Despatcher
{
    //	Id – integer, Primary Key
    //	Name – text with length[2, 40] (required)
    //	Position – text
    //	Trucks – collection of type Truck

    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(40)]
    public string Name { get; set; } = null!;


    [Required]
    public string Position { get; set; } = null!;        //MOJE DA NE E REQUIRED




    public virtual ICollection<Truck> Trucks { get; set; } = new List<Truck>();
}