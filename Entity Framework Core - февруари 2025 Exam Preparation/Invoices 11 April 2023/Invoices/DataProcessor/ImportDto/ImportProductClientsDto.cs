using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Invoices.DataProcessor.ImportDto;

public class ImportProductClientsDto
{

    [JsonProperty(nameof(Name))]
    [Required]
    [MaxLength(30)]
    [MinLength((9))]
    public string Name { get; set; } = null!;


    [JsonProperty(nameof(Price))]
    [Required]
    [Range(5.00f, 1000.00f)]
    public decimal Price { get; set; }



    [JsonProperty(nameof(CategoryType))]
    [Required]
    [Range(0,4)]
    public int CategoryType { get; set; }



    [JsonProperty(nameof(Clients))]
    [Required]
    public int[] Clients { get; set; } = null!;
}