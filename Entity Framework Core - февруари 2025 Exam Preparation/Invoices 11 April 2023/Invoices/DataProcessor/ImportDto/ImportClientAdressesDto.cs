using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Invoices.Data.Models;

namespace Invoices.DataProcessor.ImportDto;

[XmlType(nameof(Client))]
public class ImportClientAdressesDto
{

    [XmlElement(nameof(Name))]
    [Required]
    [MaxLength(25)]
    [MinLength(10)]
    public string Name { get; set; } = null!;




    [XmlElement(nameof(NumberVat))]
    [Required]
    [MaxLength(15)]
    [MinLength(10)]
    public string NumberVat { get; set; } = null!;




    [XmlArray(nameof(Addresses))]
    [XmlArrayItem(nameof(Address))]
    [Required]
    public ImportAdressesDto[] Addresses { get; set; } = null!;
}