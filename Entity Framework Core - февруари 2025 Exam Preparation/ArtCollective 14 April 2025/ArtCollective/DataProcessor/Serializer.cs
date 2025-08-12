using System.Xml.Serialization;
using System.Xml;
using ArtCollective.Data;
using Newtonsoft.Json;
using System.Globalization;
using ArtCollective.Data.Models.Enum;
using ArtCollective.DataProcessor.ExportDTOs;
using Microsoft.EntityFrameworkCore;
using ArtCollective.Utilities;

namespace ArtCollective.DataProcessor
{
    public class Serializer
    {
        public static string ExportArtistsWithCollaborationsCountAndTheirArtworks(ArtCollectiveDbContext dbContext)
        {
            const string xmlRoot = "Artists";

            var validArtists = dbContext.Artists
                .Select(x => new ExportArtistArtworksDto()
                {
                    Collaborations = x.Collaboration1.Count + x.Collaboration2.Count,
                    Username = x.Username,
                    Artworks = x.Artworks
                        .Select(x => new ExportArtworksDto()
                        {
                            Title = x.Title,
                            CreatedOn = x.CreatedOn.ToString("yyyy-MM-dd"),
                        })
                        .ToArray()
                })
                .OrderBy(x => x.Username)
                .ToArray();

            string result = XmlHelper.Serialize(validArtists, xmlRoot);
            return result;
        }

        public static string ExportGroupsWithFeedbacksChronologically(ArtCollectiveDbContext dbContext)
        {
            var validGroups = dbContext.Groups
                .Select(x => new ExportGroupFeedbacksDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    StartedOn = x.StartedOn.ToString("yyyy-MM-dd"),
                    Feedbacks = x.Feedbacks
                        .Select(x => new ExportFeedbacksDto()
                        {
                            Content = x.Content,
                            GivenOn = x.GivenOn.ToString("yyyy-MM-dd"),
                            Status = (int)x.Status,
                            ArtistUsername = x.Artist.Username,
                        }).ToArray()
                }).ToArray();

            string result = JsonConvert.SerializeObject(validGroups, Newtonsoft.Json.Formatting.Indented);
            return result;
        }
    }
}
