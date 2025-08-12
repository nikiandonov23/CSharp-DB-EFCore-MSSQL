using System.Xml.Serialization;
using ArtCollective.Data.Models;

namespace ArtCollective.DataProcessor.ExportDTOs;


[XmlType(nameof(Artist))]
public class ExportArtistArtworksDto
{
    [XmlAttribute(nameof(Collaborations))]
    public int Collaborations { get; set; }


    [XmlElement(nameof(Username))]
    public string Username { get; set; } = null!;


    [XmlArray(nameof(Artworks))]
    [XmlArrayItem(nameof(Artwork))]
    public ExportArtworksDto[] Artworks { get; set; } = null!;



}