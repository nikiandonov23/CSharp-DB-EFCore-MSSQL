using System.ComponentModel.DataAnnotations;

namespace Invoices.Data.Models;

public class Client
{
    //	Id – integer, Primary Key
    //	Name – text with length[10…25] (required)
    //	NumberVat – text with length[10…15] (required)
    //	Invoices – collection of type Invoicе
    //	Addresses – collection of type Address
    //	ProductsClients – collection of type ProductClient

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(25)]
    [MinLength(10)]
    public string Name { get; set; } = null!;


    [Required]
    [MaxLength(15)]
    [MinLength(10)]
    public string NumberVat { get; set; } = null!;


    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    public virtual ICollection<ProductClient> ProductsClients { get; set; } = new List<ProductClient>();
}