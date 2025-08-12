using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using ArtCollective.Data;
using ArtCollective.Data.Models;
using ArtCollective.Data.Models.Enum;
using ArtCollective.DataProcessor.ImportDTOs;
using ArtCollective.Utilities;
using Newtonsoft.Json;

namespace ArtCollective.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format.";
        private const string DuplicatedData = "Data is duplicated.";
        private const string SuccessfullyImportedFeedbackEntity = "Successfully imported feedback (Given on: {0}, Status: {1})";
        private const string SuccessfullyImportedArtworkEntity = "Successfully imported artwork (Artist: {0}, Created on: {1})";

        public static string ImportFeedbacks(ArtCollectiveDbContext dbContext, string xmlString)
        {

            const string xmlRoot = "Feedbacks";

            StringBuilder output = new StringBuilder();



            ImportFeedbacksDto[]? feedbackDtos = XmlHelper
                .Deserialize<ImportFeedbacksDto[]>(xmlString, xmlRoot);

            if (feedbackDtos != null && feedbackDtos.Length > 0)
            {


                foreach (var feedbacksDto in feedbackDtos)
                {

                    bool isGivenDateValid = DateTime
                        .TryParseExact(feedbacksDto.GivenOn, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime givenDate);



                    bool isStatusEnumValid = Enum
                        .TryParse<Status>(feedbacksDto.Status, out Status statusStatus);


                    if (!IsValid(feedbacksDto) || !isGivenDateValid || !isStatusEnumValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isGroupIdValid = dbContext.Groups
                        .Any(x => x.Id == feedbacksDto.GroupId);

                    bool isArtistIdValid = dbContext.Artists
                        .Any(x => x.Id == feedbacksDto.ArtistId);

                    if (!isGroupIdValid || !isArtistIdValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }



                    bool isFeedbackAlreadyInDb = dbContext.Feedbacks
                        .Any(x => x.Content == feedbacksDto.Content &&
                                  x.GivenOn == givenDate &&
                                  x.Status == statusStatus &&
                                  x.ArtistId == feedbacksDto.ArtistId &&
                                  x.GroupId == feedbacksDto.GroupId);

                    if (isFeedbackAlreadyInDb)
                    {
                        output.AppendLine(DuplicatedData);
                        continue;

                    }


                    Feedback currentFeedback = new Feedback()
                    {
                        GivenOn = givenDate,
                        Content = feedbacksDto.Content,
                        Status = statusStatus,
                        GroupId = feedbacksDto.GroupId,
                        ArtistId = feedbacksDto.ArtistId
                    };

                    dbContext.Feedbacks.Add(currentFeedback);
                    dbContext.SaveChanges();
                    output.AppendLine(string.Format(SuccessfullyImportedFeedbackEntity, givenDate.ToString("yyyy-MM-dd"), statusStatus));

                }


            }


            return output.ToString().TrimEnd();
        }

        public static string ImportArtworks(ArtCollectiveDbContext dbContext, string jsonString)
        {
            StringBuilder output = new StringBuilder();


            ImportPostsDto[]? artworkDtos = JsonConvert.DeserializeObject<ImportPostsDto[]>(jsonString);


            if (artworkDtos != null && artworkDtos.Length > 0)
            {
                foreach (var artworkDto in artworkDtos)
                {

                    bool isCreatedOnDateValid = DateTime
                        .TryParseExact(artworkDto.CreatedOn, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime createdOnDate);




                    bool isArtistExist = dbContext.Artists
                        .Any(x => x.Id == artworkDto.ArtistId);

                    if (!IsValid(artworkDto) || !isCreatedOnDateValid || !isArtistExist)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }




                    bool isTitleAlreadyExist = dbContext.Artworks
                        .Any(x => x.Title == artworkDto.Title &&
                                  x.ArtistId == artworkDto.ArtistId);


                    if (isTitleAlreadyExist)
                    {
                        output.AppendLine(DuplicatedData);
                        continue;
                    }


                    Artwork currentArtwork = new Artwork()
                    {
                        Title = artworkDto.Title,
                        Description = artworkDto.Description,
                        CreatedOn = createdOnDate,
                        Artist = new Artist()
                       
                    };

                    var currentArtist = dbContext.Artists
                        .First(x => x.Id == artworkDto.ArtistId);
                    currentArtwork.Artist= currentArtist;



                    dbContext.Artworks.Add(currentArtwork);
                    dbContext.SaveChanges();
                    output.AppendLine(string.Format(SuccessfullyImportedArtworkEntity, currentArtist.Username,
                        createdOnDate.ToString("yyyy-MM-dd")));



                }



            }


            return output.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            ValidationContext validationContext = new ValidationContext(dto);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            List<string> errorMessages = new List<string>();
            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            errorMessages = validationResults.Select(r => r.ErrorMessage!).ToList();

            return isValid;
        }
    }
}
