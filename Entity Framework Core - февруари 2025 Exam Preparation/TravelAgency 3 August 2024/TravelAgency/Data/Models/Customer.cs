

namespace TravelAgency.Data.Models;
using System.ComponentModel.DataAnnotations;

public class Customer
{
  

    

    
    [Key]
    public int Id { get; set; }



    [Required]
    [MaxLength(60)]
    [MinLength(4)]
    public string FullName { get; set; } = string.Empty;





    [Required]
    [MaxLength(50)]
    [MinLength(6)]
    public string Email { get; set; } = string.Empty;





    [Required]
    [RegularExpression(@"^\+\d{12}$")]   //да не гърми само регекса....
    public string PhoneNumber { get; set; } = string.Empty;






    //todo //   	Bookings - a collection of type Booking

    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();


}