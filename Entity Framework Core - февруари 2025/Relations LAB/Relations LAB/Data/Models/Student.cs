using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relations_LAB.Data.Models;

[Table("Students", Schema = "uni")]
public class Student
{
    [Key]
    public int StudentPk { get; set; }


    public string Name { get; set; } = string.Empty;

    public string FacultyNumber { get; set; }

    public int Semester { get; set; }


    public int AddressId { get; set; }
    public Address Address { get; set; } = null!;





    public int? RoomId { get; set; }

    public Room? Room { get; set; }

    public IList<StudentsCourses> StudentsCourses { get; set; }=new List<StudentsCourses>();


  


}