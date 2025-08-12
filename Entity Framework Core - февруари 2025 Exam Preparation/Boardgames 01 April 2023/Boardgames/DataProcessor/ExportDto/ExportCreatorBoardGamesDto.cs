using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ExportDto;


[XmlType(nameof(Creator))]
public class ExportCreatorBoardGamesDto
{


    [XmlAttribute(nameof(BoardgamesCount))]
    public int BoardgamesCount { get; set; }


    [XmlElement(nameof(CreatorName))]
    public string CreatorName { get; set; } = null!;



    [XmlArray(nameof(Boardgames))]
    [XmlArrayItem("Boardgame")]
    public ExportBoardGamesXMLDto[] Boardgames { get; set; } = null!;
}