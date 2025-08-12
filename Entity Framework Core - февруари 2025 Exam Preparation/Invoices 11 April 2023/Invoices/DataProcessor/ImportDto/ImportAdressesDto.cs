using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Invoices.Data.Models;

namespace Invoices.DataProcessor.ImportDto;


[XmlType(nameof(Address))]
public class ImportAdressesDto
{
    [XmlElement(nameof(StreetName))]
    [Required]
    [MaxLength(20)]
    [MinLength(10)]
    public string StreetName { get; set; } = null!;


    [XmlElement(nameof(StreetNumber))]
    [Required]
    public int StreetNumber { get; set; }


    [XmlElement(nameof(PostCode))]
    [Required]
    public string PostCode { get; set; } = null!;



    [XmlElement(nameof(City))]
    [Required]
    [MaxLength(15)]
    [MinLength(5)]
    public string City { get; set; } = null!;



    [XmlElement(nameof(Country))]
    [Required]
    public string Country { get; set; } = null!;
}