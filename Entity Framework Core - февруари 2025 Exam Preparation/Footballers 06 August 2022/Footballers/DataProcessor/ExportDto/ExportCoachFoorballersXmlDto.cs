using System.Xml.Serialization;
using Footballers.Data.Models;

namespace Footballers.DataProcessor.ExportDto;


[XmlType(nameof(Coach))]
public class ExportCoachFoorballersXmlDto
{

    [XmlAttribute(nameof(FootballersCount))]
    public int FootballersCount { get; set; }



    [XmlElement(nameof(CoachName))]
    public string CoachName { get; set; } = null!;



    [XmlArray(nameof(Footballers))]
    [XmlArrayItem(nameof(Footballer))]
    public ExportFootballersXmlDto[] Footballers { get; set; } = null!;
}