using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P03_SalesDatabase.Data.Models;

public class Customer
{
   
    

    [Key] 
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(100)]
    [Unicode]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    [Unicode(false)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string CreditCardNumber { get; set; } = string.Empty;//MOJE DA GURMI ZARADI INTERVAL 




    
    public virtual  ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
}