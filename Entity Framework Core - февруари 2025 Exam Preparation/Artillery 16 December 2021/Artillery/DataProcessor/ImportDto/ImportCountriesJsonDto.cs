using Newtonsoft.Json;

namespace Artillery.DataProcessor.ImportDto;

public class ImportCountriesJsonDto
{



    [JsonProperty(nameof(Id))] 
    public int Id { get; set; }
}