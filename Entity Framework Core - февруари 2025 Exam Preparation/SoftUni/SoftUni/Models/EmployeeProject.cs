using Microsoft.EntityFrameworkCore.Metadata;

namespace SoftUni.Models;

public class EmployeeProject
{
    public int EmployeeId { get; set; }  //propartito ot bazata danni
    public virtual Employee Employee { get; set; } = null!; //navigationalno property


    public int ProjectId { get; set; }   //propartito ot bazata danni
    public virtual Project Project { get; set; } = null!; //navigationalno property

}