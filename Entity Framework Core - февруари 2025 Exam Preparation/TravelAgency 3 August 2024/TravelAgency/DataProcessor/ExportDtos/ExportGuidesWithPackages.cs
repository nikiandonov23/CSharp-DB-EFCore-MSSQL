using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ExportDtos;


[XmlType(nameof(Guide))]
public class ExportGuidesWithPackages
{

    [XmlElement(nameof(FullName))]
    public string FullName { get; set; } = null!;


    [XmlArray(nameof(TourPackages))]
    [XmlArrayItem(nameof(TourPackage))]
    public List<ExportTourPackages> TourPackages { get; set; } = null!; //ICollection от ДТО-то дете
}