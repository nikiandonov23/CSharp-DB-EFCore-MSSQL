using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType(nameof(Creator))]
public class ImportCreatorBoardgamesDto
{
    [XmlElement(nameof(FirstName))]
    [Required]
    [MaxLength(7)]
    [MinLength(2)]
    public string FirstName { get; set; } = null!;   //ok




    [XmlElement(nameof(LastName))]
    [Required]
    [MaxLength(7)]
    [MinLength(2)]
    public string LastName { get; set; } = null!;    //ok



    [XmlArray(nameof(Boardgames))]
    [XmlArrayItem("Boardgame")]
    public ImportBoardgamesDto[] Boardgames { get; set; } = null!;
}