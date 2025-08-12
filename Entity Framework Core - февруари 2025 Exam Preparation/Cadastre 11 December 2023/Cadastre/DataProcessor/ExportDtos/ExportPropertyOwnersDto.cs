using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Cadastre.DataProcessor.ExportDtos;

public class ExportPropertyOwnersDto
{


    [JsonProperty(nameof(PropertyIdentifier))]
    [Required]
    [MaxLength(20)]
    [MinLength(16)]
    public string PropertyIdentifier { get; set; } = null!;


    [JsonProperty(nameof(Area))]
    [Required]
    [Range(0, int.MaxValue)]
    public int Area { get; set; }


    [JsonProperty(nameof(Address))]
    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    public string Address { get; set; } = null!;


    [JsonProperty(nameof(DateOfAcquisition))]
    [Required]
    public string DateOfAcquisition { get; set; } = null!;



    [JsonProperty(nameof(Owners))]
    [Required]
    public ICollection<ExportOwnersDto> Owners = new List<ExportOwnersDto>();
}