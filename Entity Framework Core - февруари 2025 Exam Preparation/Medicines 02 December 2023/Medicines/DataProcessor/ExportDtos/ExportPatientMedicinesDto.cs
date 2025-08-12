using System.Xml.Serialization;
using Medicines.Data.Models;

namespace Medicines.DataProcessor.ExportDtos;


[XmlType(nameof(Patient))]
public class ExportPatientMedicinesDto
{
    [XmlAttribute(nameof(Gender))]
    public string Gender { get; set; } = null!;




    [XmlElement(nameof(Name))]
    public string Name { get; set; } = null!;



    [XmlElement(nameof(AgeGroup))]
    public string AgeGroup { get; set; } = null!;




    [XmlArray("Medicines")]
    [XmlArrayItem("Medicine")]
    public ExportMedicineDto[] Medicines { get; set; } = null!;






}