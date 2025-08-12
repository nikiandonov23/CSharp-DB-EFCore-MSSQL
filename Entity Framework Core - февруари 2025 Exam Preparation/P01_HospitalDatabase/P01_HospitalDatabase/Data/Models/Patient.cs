using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models;

public class Patient
{

    [Key]
    public int PatientId { get; set; }

    [Required]
    [MaxLength(50)]
    [Unicode]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    [Unicode]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    [Unicode]
    public string Address { get; set; } = string.Empty;



    [Required]
    [MaxLength(80)]
    [Unicode(false)]
    public string Email { get; set; } = string.Empty;


    [Required]
    public bool HasInsurance { get; set; }


    public virtual ICollection<Visitation> Visitations { get; set; } = new HashSet<Visitation>();
    public virtual ICollection<Diagnose> Diagnoses { get; set; } = new HashSet<Diagnose>();

    public virtual ICollection<PatientMedicament> Prescriptions { get; set; } = new HashSet<PatientMedicament>();


}