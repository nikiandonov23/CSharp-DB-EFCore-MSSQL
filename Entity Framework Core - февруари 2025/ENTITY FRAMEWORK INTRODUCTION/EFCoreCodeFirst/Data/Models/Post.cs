using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirst.Data.Models;


[Comment("Articles posted by my blog")]
public class Post
{
    [Key]
    [Comment("Record Identifier")]
    public int Id { get; set; }


    [Required]
    [MaxLength(100)]
    [Comment("Title of the post.Maximum 100 symbols")]
    public string Title { get; set; } = null!;


    [Required]
    [MaxLength(4096)]
    [Comment("Content of the post.Max length 4096")]
    public string Content { get; set; }= null!;



    [Required]
    [MaxLength(150)]
    [Comment("Author of the post.Maximum 150 symbols")]
    public string Author { get; set; } = null!;

}