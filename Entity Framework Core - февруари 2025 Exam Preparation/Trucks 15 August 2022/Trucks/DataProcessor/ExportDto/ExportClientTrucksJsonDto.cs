using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Trucks.DataProcessor.ExportDto;

public class ExportClientTrucksJsonDto
{

    [JsonProperty(nameof(Name))]
    [Required]
    [MaxLength(40)]
    [MinLength(3)]
    public string Name { get; set; } = null!;


    [JsonProperty(nameof(Trucks))]
    [Required]
    public ExportTrucksJsonDto[] Trucks { get; set; } = null!;

}