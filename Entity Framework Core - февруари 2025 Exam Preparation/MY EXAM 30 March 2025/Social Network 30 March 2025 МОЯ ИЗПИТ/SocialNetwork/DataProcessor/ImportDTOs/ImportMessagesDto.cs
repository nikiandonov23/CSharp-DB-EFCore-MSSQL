using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using SocialNetwork.Data.Models;

namespace SocialNetwork.DataProcessor.ImportDTOs;


[XmlType(nameof(Message))]
public class ImportMessagesDto
{

    [XmlAttribute(nameof(SentAt))]
    [Required]
    public string SentAt { get; set; } = null!;



    [XmlElement(nameof(Content))]
    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    public string Content { get; set; } = null!;




    [XmlElement(nameof(Status))]
    [Required]
    public string Status { get; set; } = null!;




    [XmlElement(nameof(ConversationId))]
    [Required]
    public int ConversationId { get; set; }




    [XmlElement(nameof(SenderId))]
    [Required]
    public int SenderId { get; set; }

}