using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Data.Models;

public class Address
{
    //	Id – integer, Primary Key
    //	StreetName – text with length[10…20] (required)
    //	StreetNumber – integer(required)
    //  PostCode – text(required)
    // 	City – text with length[5…15] (required)
    //	Country – text with length[5…15] (required)
    //	ClientId – integer, foreign key(required)
    //  Client – Client

    [Key]
    public int Id { get; set; }



    [Required]
    [MaxLength(20)]
    [MinLength(10)]
    public string StreetName { get; set; } = null!;

    [Required]
    public int StreetNumber { get; set; }



    [Required]
    public string PostCode { get; set; } = null!;


    [Required]
    [MaxLength(15)]
    [MinLength(5)]
    public string City { get; set; } = null!;

    [Required]
    [MaxLength(15)]
    [MinLength(5)]
    public string Country { get; set; } = null!;




    [ForeignKey(nameof(Client))]
    [Required]
    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;

}