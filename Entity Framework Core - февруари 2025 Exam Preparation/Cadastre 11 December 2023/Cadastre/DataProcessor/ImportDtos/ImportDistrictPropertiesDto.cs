using Cadastre.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ImportDtos;

[XmlType(nameof(District))]
public class ImportDistrictPropertiesDto
{
    [XmlAttribute(nameof(Region))]
    [Required]
    public string Region { get; set; } = null!;

    [Required]
    [MaxLength(80)]
    [MinLength(2)]
    public string Name { get; set; } = null!;



    [Required]
    [RegularExpression(@"^[A-Z]{2}-\d{5}$")]
    public string PostalCode { get; set; } = null!;





    [XmlArray(nameof(Properties))]
    public ImportPropertyDto[] Properties { get; set; } = null!;
}