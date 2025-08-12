using System.Text;
using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ImportDto;
using Boardgames.Utilities;
using Newtonsoft.Json;

namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using Boardgames.Data;
   
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            const string xmlRoot = "Creators";

            StringBuilder output = new StringBuilder();

            ImportCreatorBoardgamesDto[]? creatorDtos = XmlHelper
                .Deserialize<ImportCreatorBoardgamesDto[]>(xmlString, xmlRoot);

            ICollection<Creator> validCreators = new List<Creator>();

            if (creatorDtos!=null&&creatorDtos.Length>0)
            {

                foreach (var creatorDto in creatorDtos)
                {

                    if (!IsValid(creatorDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    Creator currentCreator = new Creator() //Creator s prazna kolekciq
                    {
                        FirstName = creatorDto.FirstName,
                        LastName = creatorDto.LastName,
                        Boardgames = new List<Boardgame>()

                    };

                    foreach (var boardgame in creatorDto.Boardgames)
                    {
                        if (!IsValid(boardgame))
                        {
                            output.AppendLine(ErrorMessage);
                            continue;

                        }

                        Boardgame currentBoardgame = new Boardgame()
                        {
                            Name = boardgame.Name,
                            Rating = boardgame.Rating,
                            YearPublished = boardgame.YearPublished,
                            CategoryType = (CategoryType)boardgame.CategoryType,
                            Mechanics = boardgame.Mechanics
                        };

                        currentCreator.Boardgames.Add(currentBoardgame);
                    }

                    validCreators.Add(currentCreator);
                    output.AppendLine(string
                        .Format(SuccessfullyImportedCreator, currentCreator.FirstName, currentCreator.LastName,
                            currentCreator.Boardgames.Count));
                }
                context.AddRange(validCreators);
                context.SaveChanges();
            }

            return output.ToString().TrimEnd();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();

            ImportSellerBoardgamesDto[]? sellerDtos = JsonConvert
                .DeserializeObject<ImportSellerBoardgamesDto[]>(jsonString);

            ICollection<Seller> validSellers = new List<Seller>();


            if (sellerDtos!=null&&sellerDtos.Length>0)
            {
                foreach (var sellerDto in sellerDtos)
                {
                    if (!IsValid(sellerDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }



                    Seller currentSeller = new Seller()   //seller s prazen array 
                    {
                        Name = sellerDto.Name,
                        Address = sellerDto.Address,
                        Country = sellerDto.Country,
                        Website = sellerDto.Website,
                        BoardgamesSellers = new List<BoardgameSeller>()

                    };

                    
                    foreach (var boardgameInt in sellerDto.Boardgames.Distinct())
                    {
                        

                        BoardgameSeller currentBoardgame = new BoardgameSeller()
                        {
                            BoardgameId = boardgameInt
                        };

                        bool isBoardgameExistInDB = context.Boardgames
                            .Any(x => x.Id == currentBoardgame.BoardgameId);

                        if (!isBoardgameExistInDB)
                        {
                            output.AppendLine(ErrorMessage);
                            continue;
                        }

                        currentSeller.BoardgamesSellers.Add(currentBoardgame);
                    }

                    validSellers.Add(currentSeller);
                    output.AppendLine(string.Format(SuccessfullyImportedSeller, currentSeller.Name,
                        currentSeller.BoardgamesSellers.Count));


                }

                context.AddRange(validSellers);
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
