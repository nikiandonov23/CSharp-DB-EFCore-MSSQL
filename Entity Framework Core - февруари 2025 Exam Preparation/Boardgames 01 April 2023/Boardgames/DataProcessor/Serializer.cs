using Boardgames.Data.Models;
using Boardgames.DataProcessor.ExportDto;
using Microsoft.EntityFrameworkCore;

namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.Utilities;
    using Newtonsoft.Json;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {

            const string xmlRoot = "Creators";

            var validCreators = context.Creators
                .Where(x => x.Boardgames.Count > 0)
                .Select(x => new ExportCreatorBoardGamesDto()
                {

                    CreatorName = x.FirstName + " " + x.LastName, //moje da grumneeee
                    BoardgamesCount = x.Boardgames.Count,
                    Boardgames = x.Boardgames
                        .Select(x => new ExportBoardGamesXMLDto()
                        {
                            BoardgameName = x.Name,
                            BoardgameYearPublished = x.YearPublished,

                        }).OrderBy(x=>x.BoardgameName)
                        .ToArray()


                }).OrderByDescending(x=>x.BoardgamesCount)
                .ThenBy(x=>x.CreatorName)
                .ToArray();



           
            string result = XmlHelper.Serialize(validCreators, xmlRoot);
            return result;
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var validSellers = context.Sellers
                .Where(x => x.BoardgamesSellers
                    .Any(x => x.Boardgame.YearPublished >= year && x.Boardgame.Rating <= rating))
                .Select(x => new ExportSellerBoardgamesDto()
                {

                    Name = x.Name,
                    Website = x.Website,
                    Boardgames = x.BoardgamesSellers
                        .Where(x=>x.Boardgame.YearPublished>=year&&x.Boardgame.Rating<=rating)
                        .Select(x => new ExportBoardgamesDto()
                        {
                            Name = x.Boardgame.Name,
                            Rating = x.Boardgame.Rating,
                            Mechanics = x.Boardgame.Mechanics,
                            Category = x.Boardgame.CategoryType.ToString()

                        }).OrderByDescending(x=>x.Rating)
                        .ThenBy(x=>x.Name)
                        .ToArray()
                }).OrderByDescending(x=>x.Boardgames.Length)
                .ThenBy(x => x.Name)
                .ToArray()
                .Take(5);

            string result = JsonConvert.SerializeObject(validSellers, Formatting.Indented);
            return result;
        }
    }
}