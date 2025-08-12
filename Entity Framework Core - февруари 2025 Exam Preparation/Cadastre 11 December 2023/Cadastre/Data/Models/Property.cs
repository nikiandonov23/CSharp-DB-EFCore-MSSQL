using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastre.Data.Models;

public class Property
{
    
    // 	Details - text with length[5, 500] (not required)
    //	Address – text with length[5, 200] (required)
    //	DateOfAcquisition – DateTime(required)
    // 	DistrictId – integer, foreign key(required)
    // 	District – District
    //	PropertiesCitizens - collection of type PropertyCitizen
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    [MinLength(16)]
    public string PropertyIdentifier { get; set; } = null!;

    [Required]
    [Range(0,int.MaxValue)]
    public int Area { get; set; }


    [MinLength(5)]
    [MaxLength(500)]
    public string? Details { get; set; }


    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    [Required]
    public DateTime DateOfAcquisition  { get; set; }




    [ForeignKey(nameof(District))]
    public int DistrictId  { get; set; }
    public virtual District District { get; set; } = null!;




    public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; } = new HashSet<PropertyCitizen>();

}