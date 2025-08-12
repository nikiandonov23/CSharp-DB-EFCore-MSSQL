using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.DataProcessor.ImportDTOs;

public class ImportPostsDto
{
    [JsonProperty(nameof(Content))]
    [Required]
    [MaxLength(300)]
    [MinLength(5)]
    public string Content { get; set; } = null!;




    [JsonProperty(nameof(CreatedAt))]
    [Required]
    public string CreatedAt { get; set; } = null!;




    [JsonProperty(nameof(CreatorId))]
    [Required]
    public int CreatorId { get; set; }
}