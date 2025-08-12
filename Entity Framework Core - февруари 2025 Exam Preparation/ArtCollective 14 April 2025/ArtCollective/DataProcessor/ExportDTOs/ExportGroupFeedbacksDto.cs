using Newtonsoft.Json;

namespace ArtCollective.DataProcessor.ExportDTOs;

public class ExportGroupFeedbacksDto
{

    [JsonProperty(nameof(Id))]
    public int Id { get; set; }






    [JsonProperty(nameof(Title))]
    public string Title { get; set; } = null!;





    [JsonProperty(nameof(StartedOn))]
    public string StartedOn { get; set; } = null!;





    [JsonProperty(nameof(Feedbacks))]
    public ExportFeedbacksDto[] Feedbacks { get; set; } = null!;

}