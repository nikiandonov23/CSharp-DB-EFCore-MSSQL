using System.ComponentModel.DataAnnotations;

namespace NetPay.Data.Models;

public class Supplier
{
    //	Id – integer, Primary Key
    //	SupplierName – text with length[3, 60] (required)
    //	SuppliersServices - collection of type SupplierService

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(60)]
    [MinLength(3)]
    public string SupplierName { get; set; } = null!;



    public virtual ICollection<SupplierService> SuppliersServices { get; set; } = new List<SupplierService>();
}