using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto;


[XmlType(nameof(Footballers))]
public class ImportFootballersDto
{

    [XmlElement(nameof(Name))]
    [Required]
    [MaxLength(40)]
    [MinLength(2)]
    public string Name { get; set; } = null!;



    [XmlElement(nameof(ContractStartDate))]
    [Required]
    public string ContractStartDate { get; set; } = null!;



    [XmlElement(nameof(ContractEndDate))]
    [Required]
    public string ContractEndDate { get; set; } = null!;




    [XmlElement(nameof(BestSkillType))]
    [Required]
    [Range(0, 4)]
    public int BestSkillType { get; set; }  //ENUM




    [XmlElement(nameof(PositionType))]
    [Required]
    [Range(0, 3)]
    public int PositionType { get; set; }   //ENUM
}