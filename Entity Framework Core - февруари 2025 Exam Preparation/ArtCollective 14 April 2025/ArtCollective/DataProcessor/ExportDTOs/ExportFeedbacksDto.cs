using Newtonsoft.Json;

namespace ArtCollective.DataProcessor.ExportDTOs;

public class ExportFeedbacksDto
{

    [JsonProperty(nameof(Content))]
    public string Content { get; set; } = null!;



    [JsonProperty(nameof(GivenOn))]
    public string GivenOn { get; set; } = null!;



    [JsonProperty(nameof(Status))]
    public int Status { get; set; }




    [JsonProperty(nameof(ArtistUsername))]
    public string ArtistUsername { get; set; } = null!;
}