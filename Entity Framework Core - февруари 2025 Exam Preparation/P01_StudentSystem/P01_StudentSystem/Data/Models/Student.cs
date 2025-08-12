using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models;

public class Student
{
    [Key]
    public int StudentId { get; set; }


    [Unicode]
    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;

   

    private string? _phoneNumber;

    [Unicode(false)]
    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            //  точно 10 символаааа
            if (value != null && value.Length == 10)
            {
                _phoneNumber = value;
            }
            else
            {
                _phoneNumber = null;
            }
        }
    }

    [Required]
    public DateTime RegisteredOn { get; set; }

    public DateTime? Birthday  { get; set; }   //not required


    public virtual ICollection<StudentCourse> StudentsCourses { get; set; } = new HashSet<StudentCourse>();


    public virtual ICollection<Homework> Homeworks { get; set; }=new HashSet<Homework>();
}