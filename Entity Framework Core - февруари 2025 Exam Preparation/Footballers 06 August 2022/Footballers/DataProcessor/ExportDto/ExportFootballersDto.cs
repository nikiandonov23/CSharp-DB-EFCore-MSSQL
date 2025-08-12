using Newtonsoft.Json;

namespace Footballers.DataProcessor.ExportDto;

public class ExportFootballersDto
{

    [JsonProperty(nameof(FootballerName))]
    public string FootballerName { get; set; } = null!;


    [JsonProperty(nameof(ContractStartDate))]
    public string ContractStartDate { get; set; } = null!;


    [JsonProperty(nameof(ContractEndDate))]
    public string ContractEndDate { get; set; } = null!;


    [JsonProperty(nameof(BestSkillType))]

    public string BestSkillType { get; set; } = null!;



    [JsonProperty(nameof(PositionType))]

    public string PositionType { get; set; } = null!;
}