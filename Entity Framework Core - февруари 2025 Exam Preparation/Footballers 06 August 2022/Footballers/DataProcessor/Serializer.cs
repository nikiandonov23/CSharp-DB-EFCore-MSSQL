using Footballers.Data.Models.Enums;
using Footballers.DataProcessor.ExportDto;


namespace Footballers.DataProcessor
{
    using Data;
    using Footballers.Utilities;
    using Newtonsoft.Json;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {

            const string xmlRoot = "Coaches";

            var validCoaches = context.Coaches
                .Where(x => x.Footballers.Count > 0)
                .Select(x => new ExportCoachFoorballersXmlDto()
                {
                    FootballersCount = x.Footballers.Count,
                    CoachName = x.Name,
                    Footballers = x.Footballers
                        .Select(x => new ExportFootballersXmlDto()
                        {
                            Name = x.Name,
                            Position = x.PositionType.ToString()

                        })
                        .OrderBy(x => x.Name)
                        .ToArray()



                }).OrderByDescending(x => x.Footballers.Length)
                .ThenBy(x => x.CoachName)
                .ToArray();



            string result = XmlHelper.Serialize(validCoaches, xmlRoot);
            return result;

           
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context
                .Teams
                .Where(t => t.TeamsFootballers.Any(tf => tf.Footballer.ContractStartDate >= date))
                .ToArray()
                .Select(t => new
                {
                    t.Name,
                    Footballers = t.TeamsFootballers
                        .Where(tf => tf.Footballer.ContractStartDate >= date)
                        .ToArray()
                        .OrderByDescending(tf => tf.Footballer.ContractEndDate)
                        .ThenBy(tf => tf.Footballer.Name)
                        .Select(tf => new
                        {
                            FootballerName = tf.Footballer.Name,
                            ContractStartDate = tf.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                            ContractEndDate = tf.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                            BestSkillType = tf.Footballer.BestSkillType.ToString(),
                            PositionType = tf.Footballer.PositionType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(t => t.Footballers.Length)
                .ThenBy(t => t.Name)
                .Take(5)
                .ToArray();



            string result = JsonConvert.SerializeObject(teams, Formatting.Indented);
            return result;



        }
    }
}
