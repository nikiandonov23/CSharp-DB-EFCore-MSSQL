using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SocialNetwork.DataProcessor.ExportDTOs;

public class ExportMessagesDto
{
    [JsonProperty(nameof(Content))]
    public string Content { get; set; } = null!;


    [JsonProperty(nameof(SentAt))]
    public string SentAt { get; set; } = null!;



    [JsonProperty(nameof(Status))]
    public int Status { get; set; } 




    [JsonProperty(nameof(SenderUsername))]
    public string SenderUsername { get; set; } = null!;
}