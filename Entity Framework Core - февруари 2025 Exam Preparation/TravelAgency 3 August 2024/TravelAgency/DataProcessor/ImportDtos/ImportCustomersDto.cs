using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ImportDtos;


[XmlType(nameof(Customer))]
public class ImportCustomersDto
{


 
    [XmlAttribute("phoneNumber")]
    [Required]
    [RegularExpression(@"^\+\d{12}$")]
    public string PhoneNumber { get; set; } = null!;


    [XmlElement(nameof(FullName))]
    [Required]
    [MaxLength(60)]
    [MinLength(4)]
    public string FullName { get; set; } = null!;


    [XmlElement(nameof(Email))]
    [Required]
    [MaxLength(50)]
    [MinLength(6)]
    public string Email { get; set; } = null!;



}