using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Relations_LAB.Data.Models;



[Table("Courses",Schema = "uni")]
[Comment("Courses in university")]
public class Course
{
    [Key]
    public int Id { get; set; }


    [Required] [MaxLength(100)] [Unicode]
    [Comment("The name of the current course")]
    public string CourseName { get; set; } = string.Empty;

    public DateTime CourseStart { get; set; }
    public DateTime? CourseEnd { get; set; }





    public  IList<StudentsCourses> StudentsCourses { get; set; }= new List<StudentsCourses>();
}