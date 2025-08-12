using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Artillery.DataProcessor.ImportDto;

public class ImportGunCountriesJsonDto
{
    [JsonProperty(nameof(ManufacturerId))]
    [Required]
    public int ManufacturerId { get; set; }


    [JsonProperty(nameof(GunWeight))]
    [Required]
    [Range(100, 1350000)]
    public int GunWeight { get; set; }



    [JsonProperty(nameof(BarrelLength))]
    [Required]
    [Range(2.00, 35.00)]
    public double BarrelLength { get; set; }



    [JsonProperty(nameof(NumberBuild))]
    public int? NumberBuild { get; set; }    //MOJE DA ISKA DA E REQUIRED BA LI GO.....




    [JsonProperty(nameof(Range))]
    [Required]
    [Range(1, 100000)]
    public int Range { get; set; }


    [JsonProperty(nameof(GunType))]
    [Required]
    public string GunType { get; set; } = null!; //ENUM DA SE VALIDIRA V SERIALIZERA !!!!

    [JsonProperty(nameof(ShellId))]
    [Required]
    public int ShellId { get; set; }



    [JsonProperty(nameof(Countries))]
    [Required]
    public ImportCountriesJsonDto[] Countries { get; set; } = null!;

}