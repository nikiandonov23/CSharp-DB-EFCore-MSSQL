using Newtonsoft.Json;

namespace SocialNetwork.DataProcessor.ExportDTOs;

public class ExportConversationMessagesDto
{
    [JsonProperty(nameof(Id))]
    public int Id { get; set; }


    [JsonProperty(nameof(Title))]
    public string Title { get; set; } = null!;



    [JsonProperty(nameof(StartedAt))]
    public string StartedAt { get; set; } = null!;



    [JsonProperty(nameof(Messages))]
    public ExportMessagesDto[] Messages { get; set; } = null!;
}