using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Cadastre.Data.Models;

namespace Cadastre.DataProcessor.ImportDtos;

[XmlType(nameof(Property))]
public class ImportPropertyDto
{
    [Required]
    [MaxLength(20)]
    [MinLength(16)]
    [XmlElement(nameof(PropertyIdentifier))]
    public string PropertyIdentifier { get; set; } = null!;



    [Required]
    [XmlElement(nameof(Area))]
    [Range(0, int.MaxValue)]
    public int Area { get; set; }  //МОЖЕ ДА СЕ УСЕРЕ


    [MinLength(5)]
    [MaxLength(500)]
    [XmlElement(nameof(Details))]
    public string? Details { get; set; }




    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    [XmlElement(nameof(Address))]
    public string Address { get; set; } = null!;



    [Required]
    [XmlElement(nameof(DateOfAcquisition))]
    public string DateOfAcquisition { get; set; } = null!;   //НЕ МОГА ДА ГО ВАЛИДИРАМ ТУКА 

}