using Newtonsoft.Json;

namespace Invoices.DataProcessor.ExportDto;

public class ExportClientsDto
{

    [JsonProperty(nameof(Name))]
    public string Name { get; set; } = null!;



    [JsonProperty(nameof(NumberVat))]
    public string NumberVat { get; set; } = null!;
}