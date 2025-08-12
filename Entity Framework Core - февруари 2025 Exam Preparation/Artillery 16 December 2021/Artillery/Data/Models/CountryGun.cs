using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artillery.Data.Models;

public class CountryGun
{
    //	CountryId – Primary Key integer, foreign key(required)
    // 	GunId – Primary Key integer, foreign key(required)



    [ForeignKey(nameof(Country))]
    [Required]
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;




    [ForeignKey(nameof(Gun))]
    [Required]
    public int GunId { get; set; }
    public Gun Gun { get; set; } = null!;

}