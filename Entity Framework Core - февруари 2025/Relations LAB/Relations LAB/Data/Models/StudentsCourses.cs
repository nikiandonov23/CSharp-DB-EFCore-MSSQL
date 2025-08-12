using System.ComponentModel.DataAnnotations.Schema;

namespace Relations_LAB.Data.Models;


[Table("StudentsCourses",Schema = "uni")]
public class StudentsCourses
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }

 

    [ForeignKey(nameof(StudentId))]
    public Student Student { get; set; } = null!;


    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = null!;

}