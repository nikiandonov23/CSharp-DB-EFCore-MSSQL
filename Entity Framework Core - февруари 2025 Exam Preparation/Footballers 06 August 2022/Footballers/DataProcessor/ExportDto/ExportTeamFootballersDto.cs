using Newtonsoft.Json;

namespace Footballers.DataProcessor.ExportDto;

public class ExportTeamFootballersDto
{

    [JsonProperty(nameof(Name))]
    public string Name { get; set; } = null!;



    [JsonProperty(nameof(Footballers))]
    public ExportFootballersDto[] Footballers { get; set; } = null!;
}