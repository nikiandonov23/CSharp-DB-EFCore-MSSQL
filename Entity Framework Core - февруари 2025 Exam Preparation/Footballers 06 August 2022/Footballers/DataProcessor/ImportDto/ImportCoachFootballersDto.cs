using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Data.Models;

namespace Footballers.DataProcessor.ImportDto;



[XmlType(nameof(Coach))]
public class ImportCoachFootballersDto
{

    [XmlElement(nameof(Name))]
    [Required]
    [MaxLength(40)]
    [MinLength(2)]
    public string Name { get; set; } = null!;


    [XmlElement(nameof(Nationality))]
    [Required]
    public string Nationality { get; set; } = null!;



    [XmlArray(nameof(Footballers))]
    [XmlArrayItem(nameof(Footballer))]
    public ImportFootballersDto[] Footballers { get; set; } = null!;
}