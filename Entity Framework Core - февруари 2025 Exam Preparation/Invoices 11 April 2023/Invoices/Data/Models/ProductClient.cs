using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Models;

public class ProductClient
{
    //	ProductId – integer, Primary Key, foreign key(required)
    //  	Product – Product
    //	ClientId – integer, Primary Key, foreign key(required)
    //  	Client – Client



    [ForeignKey(nameof(Product))]
    [Required]
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;



    [ForeignKey(nameof(Client))]
    [Required]
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

}