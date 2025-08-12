using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Footballers.DataProcessor.ImportDto;

public class ImportTeamFootballersDto
{

    [JsonProperty(nameof(Name))]
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9 .-]{3,40}$")]
    public string Name { get; set; } = null!;



    [JsonProperty(nameof(Nationality))]
    [Required]
    [MaxLength(40)]
    [MinLength(2)]
    public string Nationality { get; set; } = null!;



    [JsonProperty(nameof(Trophies))]
    [Required]
    public int Trophies { get; set; }



    [JsonProperty(nameof(Footballers))]
    public int[] Footballers { get; set; } = null!;
}