using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cadastre.DataProcessor.ImportDtos;

public class ImportCitizenDto
{

    [JsonProperty(nameof(FirstName))]
    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;




    [JsonProperty(nameof(LastName))]
    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;




    [JsonProperty(nameof(BirthDate))]
    [Required]
    public string BirthDate { get; set; } = null!;




    [JsonProperty(nameof(MaritalStatus))]
    [Required]
    public string MaritalStatus { get; set; } = null!;




    [JsonProperty(nameof(Properties))]
    [Required]
    public int[] Properties { get; set; } = null!;
}