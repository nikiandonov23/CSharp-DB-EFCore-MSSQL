using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models.Enumerations;

namespace P01_StudentSystem.Data.Models;

public class Resource
{
    [Key]
    public int ResourceId { get; set; }


    [Unicode]
    [MaxLength(50)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Unicode(false)]
    [Required]
    public string Url { get; set; } = string.Empty;


    [Required]
    public ResourceType ResourceType { get; set; }




    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;
}