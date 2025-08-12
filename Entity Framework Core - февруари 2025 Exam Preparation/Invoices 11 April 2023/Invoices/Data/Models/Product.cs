using System.ComponentModel.DataAnnotations;
using Invoices.Data.Models.Enums;

namespace Invoices.Data.Models;

public class Product
{

    [Key]
    public int Id { get; set; }


    [Required]
    [MaxLength(30)]
    [MinLength((9))]
    public string Name { get; set; } = null!;


    [Required]
    [Range(5.00f,1000.00f)]
    public decimal Price { get; set; }


    [Required]
    public CategoryType CategoryType { get; set; }


    [Required]
    public virtual ICollection<ProductClient> ProductsClients { get; set; } = new List<ProductClient>();
}