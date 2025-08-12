using System.Xml.Serialization;
using SocialNetwork.Data.Models;

namespace SocialNetwork.DataProcessor.ExportDTOs;



[XmlType(nameof(User))]
public class ExportUserPostsDto
{

    [XmlAttribute(nameof(Friendships))]
    public int Friendships { get; set; }


    [XmlElement(nameof(Username))]
    public string Username { get; set; } = null!;



    [XmlArray(nameof(Posts))]
    [XmlArrayItem(nameof(Post))]
    public ExportPostsDto[] Posts { get; set; } = null!;

}