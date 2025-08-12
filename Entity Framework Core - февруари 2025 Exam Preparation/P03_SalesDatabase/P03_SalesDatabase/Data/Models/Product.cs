using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P03_SalesDatabase.Data.Models;

public class Product
{
    
    
    

    [Key]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(50)] 
    [Unicode] 
    public string Name { get; set; } = string.Empty;


    public decimal Quantity { get; set; }


    public decimal Price { get; set; }

    //	

    public virtual ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

}