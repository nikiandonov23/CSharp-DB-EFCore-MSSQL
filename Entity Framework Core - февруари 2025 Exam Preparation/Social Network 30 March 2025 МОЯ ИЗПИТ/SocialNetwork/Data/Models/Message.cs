using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialNetwork.Data.Models.Enumerations;

namespace SocialNetwork.Data.Models;

public class Message
{
    //	Id – integer, Primary Key
    //	Content – text with length 1, 200] (required)
    //	SentAt – DateTime(required)
    //   •	Status – enum MessageStatus(Sent = 0, Delivered, Seen, Failed)(required)
    //	ConversationId - integer, Foreign Key(required)
    //   •	Conversation - Conversation
    //	SenderId - integer, Foreign Key(required)
    //   •	Sender – User


    [Key]
    public int Id { get; set; }


    [Required]
    [MaxLength(200)]
    [MinLength(1)]
    public string Content { get; set; } = null!;




    [Required]
    public DateTime SentAt { get; set; }




    [Required]
    public MessageStatus Status { get; set; } 





    [ForeignKey(nameof(Conversation))]
    [Required]
    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;





    [ForeignKey(nameof(Sender))]
    [Required]
    public int SenderId { get; set; }
    public User Sender { get; set; } = null!;






}