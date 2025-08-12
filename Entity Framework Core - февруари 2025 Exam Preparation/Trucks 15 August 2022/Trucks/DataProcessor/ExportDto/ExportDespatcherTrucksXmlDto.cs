using System.Xml.Serialization;
using Trucks.Data.Models;

namespace Trucks.DataProcessor.ExportDto;


[XmlType(nameof(Despatcher))]
public class ExportDespatcherTrucksXmlDto
{


    [XmlAttribute(nameof(TrucksCount))]
    public int TrucksCount { get; set; }



    [XmlElement(nameof(DespatcherName))]
    public string DespatcherName { get; set; } = null!;


    [XmlArray(nameof(Trucks))]
    [XmlArrayItem(nameof(Truck))]
    public ExportTrucksXmlDto[] Trucks { get; set; } = null!;
}