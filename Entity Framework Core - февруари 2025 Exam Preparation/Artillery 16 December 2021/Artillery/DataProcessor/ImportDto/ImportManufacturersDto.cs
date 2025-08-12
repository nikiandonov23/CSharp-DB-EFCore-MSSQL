using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Data.Models;

namespace Artillery.DataProcessor.ImportDto;


[XmlType(nameof(Manufacturer))]
public class ImportManufacturersDto
{


    [XmlElement(nameof(ManufacturerName))]
    [Required]
    [MaxLength(40)]
    [MinLength(4)]
    public string ManufacturerName { get; set; } = null!;




    [XmlElement(nameof(Founded))]
    [Required]
    [MaxLength(100)]
    [MinLength(10)]
    public string Founded { get; set; } = null!;
}