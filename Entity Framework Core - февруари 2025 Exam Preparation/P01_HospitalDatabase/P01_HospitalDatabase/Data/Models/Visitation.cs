using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models;

public class Visitation
{
    
   
    [Key]
    public int VisitationId { get; set; }



    [Required]
    public DateTime Date { get; set; }



    [Required]
    [Unicode]
    [MaxLength(250)]
    public string Comments { get; set; } = string.Empty;



    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; } = null!;


    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;


}