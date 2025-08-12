using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using NetPay.Data.Models;

namespace NetPay.DataProcessor.ExportDtos;


[XmlType(nameof(Household))]
public class ExportHouseholdExpencesXmlDto
{


    [XmlElement(nameof(ContactPerson))]
    [Required]
    public string ContactPerson { get; set; } = null!;


    [XmlElement(nameof(Email))]
    [Required]
    public string Email { get; set; } = null!;




    [XmlElement(nameof(PhoneNumber))]
    public string PhoneNumber { get; set; } = null!;





    [XmlArray(nameof(Expenses))]
    [XmlArrayItem("Expense")]
    public ExportExpencesXmlDto[] Expenses { get; set; } = null!;
}