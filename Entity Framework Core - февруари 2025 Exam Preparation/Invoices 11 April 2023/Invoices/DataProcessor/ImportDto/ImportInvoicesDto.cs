using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Invoices.DataProcessor.ImportDto;

public class ImportInvoicesDto
{

    [JsonProperty(nameof(Number))]
    [Range(1000000000, 1500000000)]
    public int Number { get; set; }



    [JsonProperty(nameof(IssueDate))]      //da se proveri POSLE
    public string IssueDate { get; set; } = null!;



    [JsonProperty(nameof(DueDate))]      //da se proveri POSLE
    public string DueDate { get; set; } = null!;



    [JsonProperty(nameof(Amount))]
    [Required]                           //OK
    public decimal Amount { get; set; }



    [JsonProperty(nameof(CurrencyType))]
    [Range(0,2)]                          //OK
    public int CurrencyType { get; set; }




    [JsonProperty(nameof(ClientId))]     //da se proveri POSLE
    public int ClientId { get; set; }


}