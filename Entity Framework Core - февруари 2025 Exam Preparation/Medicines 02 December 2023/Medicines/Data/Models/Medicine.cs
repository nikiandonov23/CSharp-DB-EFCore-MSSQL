using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Medicines.Data.Models.Enums;

namespace Medicines.Data.Models;

public class Medicine
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    [MinLength(3)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0.01, 1000.00)]
    public decimal Price { get; set; }


    [Required]
    public Category Category { get; set; }


    [Required]
    public DateTime ProductionDate { get; set; }


    [Required]
    public DateTime ExpiryDate { get; set; }



    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Producer { get; set; } = null!;




    [ForeignKey(nameof(Pharmacy))]
    public int PharmacyId { get; set; }
    public virtual Pharmacy Pharmacy { get; set; } = null!;




    public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; } = new HashSet<PatientMedicine>();
}