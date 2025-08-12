using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Medicines.Data.Models;

namespace Medicines.DataProcessor.ImportDtos;


[XmlType(nameof(Pharmacy))]
public class ImportPharmacyMedicinesDto
{
    [XmlAttribute("non-stop")]
    [RegularExpression(@"^(true|false)$")]
    [Required]
    public string NonStop { get; set; } = null!;



    [XmlElement(nameof(Name))]
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string Name { get; set; } = null!;





    [XmlElement(nameof(PhoneNumber))]
    [Required]
    [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$")]
    public string PhoneNumber { get; set; } = null!;



    [XmlArray(nameof(Medicines))]
    [XmlArrayItem(nameof(Medicine))]
    public ImportMedicineDto[] Medicines { get; set; } = null!;










}