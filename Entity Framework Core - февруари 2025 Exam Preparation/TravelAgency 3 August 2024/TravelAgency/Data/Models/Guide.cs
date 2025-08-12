using TravelAgency.Data.Models.Enums;

namespace TravelAgency.Data.Models;
using System.ComponentModel.DataAnnotations;

public class Guide
{
    
    [Key]
   public int Id { get; set; }

    [Required]
    [MaxLength(60)]
    [MinLength(4)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public Language Language { get; set; }





    //todo     //	TourPackagesGuides - collection of type TourPackageGuide

    public virtual ICollection<TourPackageGuide> TourPackagesGuides { get; set; } = new HashSet<TourPackageGuide>();

}