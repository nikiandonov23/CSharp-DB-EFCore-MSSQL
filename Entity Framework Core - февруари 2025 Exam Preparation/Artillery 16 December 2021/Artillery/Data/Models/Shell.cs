using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models;

public class Shell
{
    //	Id – integer, Primary Key
    //	ShellWeight – double in range[2…1_680] (required)
    //	Caliber – text with length[4…30] (required)
    //	Guns – a collection of Gun

    [Key]
    public int Id { get; set; }


    [Required]
    [Range(2,1680)]
    public double ShellWeight { get; set; }



    [Required]
    [MaxLength(30)]
    [MinLength(4)]
    public string Caliber { get; set; } = null!;




    public virtual ICollection<Gun> Guns { get; set; } = new HashSet<Gun>();

}