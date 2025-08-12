using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using NetPay.Data.Models;

namespace NetPay.DataProcessor.ImportDtos;


[XmlType(nameof(Household))]
public class ImportHouseholdsDto
{

    [XmlAttribute(nameof(phone))]
    [Required]
    [RegularExpression(@"^\+\d{3}/\d{3}-\d{6}$")]
    public string phone { get; set; } = null!;



    [XmlElement(nameof(ContactPerson))]
    [Required]
    [MaxLength(50)]
    [MinLength(5)]
    public string ContactPerson { get; set; } = null!;




    [XmlElement(nameof(Email))]
    [MaxLength(80)]
    [MinLength(6)]
    public string Email { get; set; } = null!;      //OSTAVQM GO NOT REQUIRED MOJE DA GURMI
}