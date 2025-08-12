using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Data.Models;

public class UserConversation
{
    //	UserId – integer, Primary Key, Foreign Key(required)
    //   •	User – User
    //	ConversationId – integer, Primary Key, Foreign Key(required)
    //   •	Conversation – Conversation

    [ForeignKey(nameof(User))]
    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;





    [ForeignKey(nameof(Conversation))]
    [Required]
    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;
}