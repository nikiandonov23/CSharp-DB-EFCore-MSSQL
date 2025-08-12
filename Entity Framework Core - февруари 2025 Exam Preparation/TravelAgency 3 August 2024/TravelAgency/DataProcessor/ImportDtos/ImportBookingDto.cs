using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ImportDtos;
public class ImportBookingDto
{

    [JsonProperty(nameof(BookingDate))]
    [Required]
    public string BookingDate { get; set; } = null!;




    [JsonProperty(nameof(CustomerName))]
    [Required]
    [MaxLength(60)]
    [MinLength(4)]
    public string CustomerName { get; set; } = null!;




    [JsonProperty(nameof(TourPackageName))]
    [Required]
    [MaxLength(40)]
    [MinLength(2)]
    public string TourPackageName { get; set; } = null!;
}