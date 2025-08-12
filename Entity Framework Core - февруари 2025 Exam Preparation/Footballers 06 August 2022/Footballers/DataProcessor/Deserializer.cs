using System.Globalization;
using System.Text;
using Footballers.Data.Models;
using Footballers.Data.Models.Enums;
using Footballers.DataProcessor.ImportDto;
using Footballers.Utilities;
using Newtonsoft.Json;

namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using System.ComponentModel.DataAnnotations;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            const string xmlRoot = "Coaches";

            StringBuilder output = new StringBuilder();

            ImportCoachFootballersDto[]? coachDtos =
                XmlHelper.Deserialize<ImportCoachFootballersDto[]>(xmlString, xmlRoot);


            ICollection<Coach> validCoaches = new List<Coach>();


            if (coachDtos!=null&&coachDtos.Length>0)
            {

                foreach (var coachDto in coachDtos)
                {
                    if (!IsValid(coachDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;


                    }


                    Coach currentCoach = new Coach()   //Coach s prazen Collection
                    {
                        Name = coachDto.Name,
                        Nationality = coachDto.Nationality,
                        Footballers = new List<Footballer>()

                    };


                    foreach (var footballer in coachDto.Footballers)
                    {
                        bool isStartDateValid = DateTime
                            .TryParseExact(footballer.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out DateTime startDate);

                        bool isEndDateValid = DateTime
                            .TryParseExact(footballer.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out DateTime endDate);






                        bool areDatesValid = startDate <=endDate;


                        if (!IsValid(footballer)|| !areDatesValid)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }


                        Footballer currentFootballer = new Footballer()
                        {
                            Name = footballer.Name,
                            ContractStartDate = startDate,
                            ContractEndDate = endDate,
                            BestSkillType = (BestSkillType)footballer.BestSkillType,
                            PositionType = (PositionType)footballer.PositionType
                        };

                        currentCoach.Footballers.Add(currentFootballer);

                    }


                    validCoaches.Add(currentCoach);
                    output.AppendLine(string.Format(SuccessfullyImportedCoach, currentCoach.Name,
                        currentCoach.Footballers.Count));

                }


                context.Coaches.AddRange(validCoaches);
                context.SaveChanges();

            }

            return output.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();


            ImportTeamFootballersDto[]?
                teamDtos = JsonConvert.DeserializeObject<ImportTeamFootballersDto[]>(jsonString);


            ICollection<Team> validTeams = new List<Team>();

            if (teamDtos!=null&&teamDtos.Length>0)
            {
                foreach (var teamDto in teamDtos)
                {
                    if (!IsValid(teamDto) || teamDto.Trophies <= 0)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Team currentTeam = new Team()    //Team s Empty MAPPING COLLECTION
                    {
                        Name = teamDto.Name,
                        Nationality = teamDto.Nationality,
                        Trophies = teamDto.Trophies,
                        TeamsFootballers = new List<TeamFootballer>()

                    };


                    foreach (var footballerInt in teamDto.Footballers.Distinct())
                    {
                        bool doesFootballerExists = context.Footballers
                            .Any(x => x.Id == footballerInt);


                        if (!doesFootballerExists)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }

                        TeamFootballer currentFooltballer = new TeamFootballer()  
                        {
                            FootballerId = footballerInt,  //RELACIQTA !!
                            Team = currentTeam    //RELACIQTA !!

                        };
                        currentTeam.TeamsFootballers.Add(currentFooltballer);
                    }



                    validTeams.Add(currentTeam);
                    output.AppendLine(string.Format(SuccessfullyImportedTeam, currentTeam.Name,
                        currentTeam.TeamsFootballers.Count));


                }

                context.Teams.AddRange(validTeams);
                context.SaveChanges();

            }


            return output.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
