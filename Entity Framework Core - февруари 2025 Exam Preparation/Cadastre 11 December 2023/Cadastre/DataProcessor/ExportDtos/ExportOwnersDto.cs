using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cadastre.DataProcessor.ExportDtos;

public class ExportOwnersDto
{
    [JsonProperty(nameof(LastName))]
    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;




    [JsonProperty(nameof(MaritalStatus))]
    [Required]
    public string MaritalStatus { get; set; } = null!;
}