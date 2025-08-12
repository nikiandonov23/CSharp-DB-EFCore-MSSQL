using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetPay.Data.Models;

public class SupplierService
{

    //	SupplierId – integer, Primary Key, foreign key(required)
    //  •	Supplier – Supplier
    //	ServiceId – integer, Primary Key, foreign key(required)
    //  •	Service – Service

    [ForeignKey(nameof(Supplier))]
    [Required]
    public int SupplierId { get; set; }

    public Supplier Supplier { get; set; } = null!;




    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;

}