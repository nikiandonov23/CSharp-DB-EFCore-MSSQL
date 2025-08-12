using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Data.Models;

public class Post
{
    //	Id – integer, Primary Key
    //	Content – text with length[5, 300] (required)
    //	CreatedAt – DateTime(required)
    //   •	CreatorId – integer, Foreign Key(required)
    //   •	Creator – User

    [Key]
    public int Id { get; set; }


    [Required]
    [MaxLength(300)]
    [MinLength(5)]
    public string Content { get; set; } = null!;




    [Required]
    public DateTime CreatedAt { get; set; }





    [ForeignKey(nameof(Creator))]
    [Required]
    public int CreatorId { get; set; }
    public User Creator { get; set; } = null!;
}