using System.Xml.Serialization;
using Boardgames.Data.Models;

namespace Boardgames.DataProcessor.ExportDto;


[XmlType(nameof(Boardgame))]
public class ExportBoardGamesXMLDto
{

    [XmlElement(nameof(BoardgameName))]
    public string BoardgameName { get; set; } = null!;



    [XmlElement(nameof(BoardgameYearPublished))]
    public int BoardgameYearPublished { get; set; }
}