using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Relations_LAB.Data.Models;



[Table("Addresses",Schema = "uni")]
public class Address
{
    
    [Key]
    public int Id { get; set; }


    [Required] 
    [MaxLength(255)] 
    [Unicode] 
    public string Town { get; set; } = string.Empty;//string.Empty значи че е задължителен


    [Required]
    [MaxLength(10)] 
    [Unicode(false)]   
    public string PostCode { get; set; } = string.Empty;//string.Empty значи че е задължителен

    [MaxLength(255)]
    [Unicode]
    public string? AddressLine { get; set; }


    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;


}