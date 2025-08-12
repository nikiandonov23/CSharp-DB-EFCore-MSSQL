using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_StudentSystem.Data.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }
    [Unicode]
    [MaxLength(80)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Unicode] 
    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }


    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public decimal Price { get; set; }


    public virtual ICollection<StudentCourse> StudentsCourses { get; set; } = new HashSet<StudentCourse>();


    public virtual ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();


    public virtual ICollection<Homework> Homeworks { get; set; } = new HashSet<Homework>();
}
