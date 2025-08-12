using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Boardgames.DataProcessor.ImportDto;

public class ImportSellerBoardgamesDto
{
    [JsonProperty(nameof(Name))]
    [Required]
    [MaxLength(20)]
    [MinLength(5)]
    public string Name { get; set; } = null!;



    [JsonProperty(nameof(Address))]
    [Required]
    [MaxLength(30)]
    [MinLength(2)]
    public string Address { get; set; } = null!;




    [JsonProperty(nameof(Country))]
    [Required]
    public string Country { get; set; } = null!;





    [JsonProperty(nameof(Website))]
    [Required]
    [RegularExpression(@"^www\.[a-zA-Z0-9-]+\.com$")]
    public string Website { get; set; } = null!;




    [JsonProperty(nameof(Boardgames))]
    [Required]
    public int[] Boardgames { get; set; } = null!;
}