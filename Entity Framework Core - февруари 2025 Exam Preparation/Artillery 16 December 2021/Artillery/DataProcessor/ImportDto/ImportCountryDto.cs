using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ImportDto;



[XmlType(nameof(Country))]
public class ImportCountryDto
{



    [XmlElement(nameof(CountryName))]
    [Required]
    [MaxLength(60)]
    [MinLength(4)]
    public string CountryName { get; set; } = null!;





    [XmlElement(nameof(ArmySize))]
    [Required]
    [Range(50000, 10000000)]
    public int ArmySize { get; set; }

}