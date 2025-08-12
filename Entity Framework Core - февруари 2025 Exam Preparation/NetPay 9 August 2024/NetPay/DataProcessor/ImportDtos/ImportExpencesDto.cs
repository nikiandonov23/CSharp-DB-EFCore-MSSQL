using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NetPay.DataProcessor.ImportDtos;

public class ImportExpencesDto
{

    [JsonProperty(nameof(ExpenseName))]
    [Required]
    [MaxLength(50)]
    [MinLength(5)]
    public string ExpenseName { get; set; } = null!;



    [JsonProperty(nameof(Amount))]
    [Required]
    [Range(0.01, 100000)]
    public decimal Amount { get; set; }



    [JsonProperty(nameof(DueDate))]
    [Required]
    public string DueDate { get; set; } = null!;   //DA SE VALIDIRA OTDELNOOOOOO



    [JsonProperty(nameof(PaymentStatus))]
    [Required]
    public string PaymentStatus { get; set; } = null!; //ENUM DA SE VALIDIRA OTDELNOOO




    [JsonProperty(nameof(HouseholdId))]
    [Required]
    public int HouseholdId { get; set; }



    [JsonProperty(nameof(ServiceId))]
    [Required]
    public int ServiceId { get; set; }

}