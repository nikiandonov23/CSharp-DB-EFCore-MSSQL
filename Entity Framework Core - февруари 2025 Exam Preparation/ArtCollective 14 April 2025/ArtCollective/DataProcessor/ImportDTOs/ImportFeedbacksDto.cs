using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using ArtCollective.Data.Models;

namespace ArtCollective.DataProcessor.ImportDTOs;


[XmlType(nameof(Feedback))]
public class ImportFeedbacksDto
{

    [XmlAttribute(nameof(GivenOn))]
    [Required]
    public string GivenOn { get; set; } = null!;   //DATETIME DA SE VALIDIRA





    [XmlElement(nameof(Content))]
    [Required]
    [MaxLength(200)]
    [MinLength(3)]
    public string Content { get; set; } = null!;





    [XmlElement(nameof(Status))]
    [Required]
    public string Status { get; set; } = null!;  //ENUM DA SE VALIDIRA






    [XmlElement(nameof(GroupId))]
    [Required]
    public int GroupId { get; set; }






    [XmlElement(nameof(ArtistId))]
    [Required]
    public int ArtistId { get; set; }


}