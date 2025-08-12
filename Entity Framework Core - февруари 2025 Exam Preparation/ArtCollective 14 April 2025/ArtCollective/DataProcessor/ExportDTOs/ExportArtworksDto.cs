using System.Xml.Serialization;
using ArtCollective.Data.Models;

namespace ArtCollective.DataProcessor.ExportDTOs;


[XmlType(nameof(Artwork))]
public class ExportArtworksDto
{
    [XmlElement(nameof(Title))]
    public string Title { get; set; } = null!;



    [XmlElement(nameof(CreatedOn))]
    public string CreatedOn { get; set; } = null!;
}