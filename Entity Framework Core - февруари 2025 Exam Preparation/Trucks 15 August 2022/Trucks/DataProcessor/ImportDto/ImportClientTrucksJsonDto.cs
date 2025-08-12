using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Trucks.DataProcessor.ImportDto;

public class ImportClientTrucksJsonDto
{


    [JsonProperty(nameof(Name))]
    [Required]
    [MaxLength(40)]
    [MinLength(3)]
    public string Name { get; set; } = null!;




    [JsonProperty(nameof(Nationality))]
    [Required]
    [MaxLength(40)]
    [MinLength(2)]
    public string Nationality { get; set; } = null!;



    [JsonProperty(nameof(Type))]
    [Required]
    public string Type { get; set; } = null!;




    [JsonProperty(nameof(Trucks))]
    [Required]
    public int[] Trucks { get; set; } = null!;
}