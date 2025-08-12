using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Trucks.Data.Models.Enums;

namespace Trucks.DataProcessor.ExportDto;

public class ExportTrucksJsonDto
{

    [JsonProperty(nameof(TruckRegistrationNumber))]
    [RegularExpression(@"^[A-Z]{2}[0-9]{4}[A-Z]{2}$")]
    [Required]
    public string TruckRegistrationNumber { get; set; } = null!;




    [JsonProperty(nameof(VinNumber))]
    [Required]
    [RegularExpression(@"^.{17}$")]
    public string VinNumber { get; set; } = null!;




    [JsonProperty(nameof(TankCapacity))]
    [Required]
    [Range(950, 1420)]
    public int TankCapacity { get; set; }




    [JsonProperty(nameof(CargoCapacity))]
    [Required]
    [Range(5000, 29000)]
    public int CargoCapacity { get; set; }




    [JsonProperty(nameof(CategoryType))]
    [Required]
    public string CategoryType { get; set; } = null!;




    [JsonProperty(nameof(MakeType))]
    [Required]
    public string MakeType { get; set; } = null!;
}