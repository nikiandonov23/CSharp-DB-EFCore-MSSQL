using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace P01_HospitalDatabase.Data.Models;

public class Diagnose
{
    

    [Key]
    public int DiagnoseId { get; set; }

    [MaxLength(50)]
    [Unicode]
    [Required]
    public string Name { get; set; } = string.Empty;

    [MaxLength(250)]
    [Unicode]
    [Required]
    public string Comments { get; set; } = string.Empty;

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }

    public virtual Patient Patient { get; set; } = null!;
}