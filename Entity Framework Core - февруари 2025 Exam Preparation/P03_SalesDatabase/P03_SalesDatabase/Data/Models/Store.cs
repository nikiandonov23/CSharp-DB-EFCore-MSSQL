using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P03_SalesDatabase.Data.Models;

public class Store
{
  
    [Key]
    public int StoreId { get; set; }

    [Required] 
    [MaxLength(80)] 
    [Unicode] 
    public string Name { get; set; } = string.Empty;


    //	
    public virtual ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
}