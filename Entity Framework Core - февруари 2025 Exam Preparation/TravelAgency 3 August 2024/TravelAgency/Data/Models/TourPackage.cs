namespace TravelAgency.Data.Models;
using System.ComponentModel.DataAnnotations;

public class TourPackage
{
    
   
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    [MinLength(2)]
    public string PackageName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)] //	Price – a positive decimal value(required)
    public decimal Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();



    //todo  //	TourPackagesGuides - collection of type TourPackageGuide

    public virtual ICollection<TourPackageGuide> TourPackagesGuides { get; set; } = new HashSet<TourPackageGuide>();
}
