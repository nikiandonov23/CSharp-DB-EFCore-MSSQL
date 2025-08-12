using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType(nameof(Boardgame))]
public class ImportBoardgamesDto
{

    [XmlElement(nameof(Name))]
    [Required]
    [MaxLength(20)]
    [MinLength(10)]
    public string Name { get; set; } = null!;   //ok



    [XmlElement(nameof(Rating))]
    [Required]
    [Range(1, 10.00)]
    public double Rating { get; set; }       //ok



    [XmlElement(nameof(YearPublished))]
    [Required]
    [Range(2018, 2023)]
    public int YearPublished { get; set; }    //ok



    [XmlElement(nameof(CategoryType))]
    [Required]
    [Range(0,4)]
    public int CategoryType { get; set; }   //ENUM  ok



    [XmlElement(nameof(Mechanics))]
    [Required]
    public string Mechanics { get; set; } = null!;   //ok
}