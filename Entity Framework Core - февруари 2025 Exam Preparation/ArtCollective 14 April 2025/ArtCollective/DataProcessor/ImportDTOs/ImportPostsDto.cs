using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ArtCollective.DataProcessor.ImportDTOs;

public class ImportPostsDto
{

    [JsonProperty(nameof(Title))]
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Title { get; set; } = null!;



    [JsonProperty(nameof(Description))]     //!!!! MOJE DA GURMI SHTOTO NE E REQUIRED !!!!!
    [MaxLength(300)]
    [MinLength(10)]
    public string Description { get; set; } = null!;



    [JsonProperty(nameof(CreatedOn))]
    [Required]
    public string CreatedOn { get; set; } = null!;  //DATETIME DA SE VALIDIRA



    [JsonProperty(nameof(ArtistId))]
    [Required]
    public int ArtistId { get; set; }
}