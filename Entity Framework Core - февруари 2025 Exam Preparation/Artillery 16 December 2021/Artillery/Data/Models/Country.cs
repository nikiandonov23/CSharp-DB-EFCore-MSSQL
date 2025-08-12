using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Artillery.Data.Models;

public class Country
{  //
   //	Id – integer, Primary Key
   //	CountryName – text with length[4, 60] (required)
   //	ArmySize – integer in the range[50_000….10_000_000] (required)
   //	CountriesGuns – a collection of CountryGun

    [Key]
   public int Id { get; set; }

    [Required]
    [MaxLength(60)]
    [MinLength(4)]
   public string CountryName { get; set; } = null!;


    [Required]
    [Range(50000,10000000)]
   public int ArmySize { get; set; }




   public ICollection<CountryGun> CountriesGuns { get; set; }=new HashSet<CountryGun>();

}