using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models;

public class Doctor
{
    [Key] public int DoctorId { get; set; }



    [Required]
    [MaxLength(100)]
    [Unicode]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Unicode]
    public string Specialty { get; set; } = string.Empty;


    public virtual ICollection<Visitation> Visitations { get; set; } = new HashSet<Visitation>();
}