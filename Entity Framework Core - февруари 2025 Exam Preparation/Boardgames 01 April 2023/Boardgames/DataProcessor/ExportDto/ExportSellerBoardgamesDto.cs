using Newtonsoft.Json;

namespace Boardgames.DataProcessor.ExportDto;

public class ExportSellerBoardgamesDto
{

    [JsonProperty(nameof(Name))]
    public string Name { get; set; } = null!;



    [JsonProperty(nameof(Website))]
    public string Website { get; set; } = null!;



    [JsonProperty(nameof(Boardgames))]
    public ExportBoardgamesDto[] Boardgames { get; set; } = null!;
}