using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Medicines.Data.Models.Enums;

namespace Medicines.DataProcessor.ImportDtos;

[XmlType("Medicine")]
public class ImportMedicineDto
{

    [XmlAttribute("category")]
    [Range(0,4)]
    [Required]
    public int Category { get; set; } //ENUMERACIQ


    [XmlElement(nameof(Name))]
    [MaxLength(150)]
    [MinLength(3)]
    [Required]
    public string Name { get; set; } = null!;


    [XmlElement(nameof(Price))]
    [Required]
    [Range(0.01f,1000.00f)]
    public decimal Price { get; set; }


    [Required]
    public string ProductionDate { get; set; } = null!; //DA SE VALIDIRA POSLEEEEEEEE



    [Required]
    public string ExpiryDate { get; set; } = null!; //DA SE VALIDIRA POSLEEEEEEEE



    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Producer { get; set; } = null!;

}