using SocialNetwork.Data;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using SocialNetwork.Data.Models;
using SocialNetwork.Data.Models.Enumerations;
using SocialNetwork.DataProcessor.ImportDTOs;
using SocialNetwork.Utilities;

namespace SocialNetwork.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format.";
        private const string DuplicatedDataMessage = "Duplicated data.";
        private const string SuccessfullyImportedMessageEntity = "Successfully imported message (Sent at: {0}, Status: {1})";
        private const string SuccessfullyImportedPostEntity = "Successfully imported post (Creator {0}, Created at: {1})";

        public static string ImportMessages(SocialNetworkDbContext dbContext, string xmlString)
        {
            const string xmlRoot = "Messages";

            StringBuilder output = new StringBuilder();

            ImportMessagesDto[]? messageDtos = XmlHelper
                .Deserialize<ImportMessagesDto[]>(xmlString, xmlRoot);



            if (messageDtos!=null&&messageDtos.Length>0)
            {

                foreach (var messageDto in messageDtos)
                {

                    if (!IsValid(messageDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }


                    bool isSentDateValid = DateTime
                        .TryParseExact(messageDto.SentAt, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime sentAtDate);


                    bool isMessageStatusEnumValid = Enum
                        .TryParse<MessageStatus>(messageDto.Status, out MessageStatus messageStatus);


                    bool isConversationIdVladi = dbContext.Conversations.Any(x => x.Id == messageDto.ConversationId);

                    bool isSenderIdValid = dbContext.Users.Any(x => x.Id == messageDto.SenderId);


                    if (!isSentDateValid || !isMessageStatusEnumValid || !isConversationIdVladi || !isSenderIdValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }



                    bool isItDupplicated = dbContext.Messages
                        .Any(x => x.SentAt == sentAtDate &&
                                  x.Status == messageStatus &&
                                  x.SenderId == messageDto.SenderId &&
                                  x.ConversationId == messageDto.ConversationId
                        );

                    if (isItDupplicated)
                    {
                        output.AppendLine(DuplicatedDataMessage);
                        continue;
                    }


                    Message currentMessage = new Message()
                    {
                        SentAt = sentAtDate,
                        Content = messageDto.Content,
                        Status = messageStatus,
                        ConversationId = messageDto.ConversationId,
                        SenderId = messageDto.SenderId,
                    };


                    dbContext.Messages.Add(currentMessage);
                    dbContext.SaveChanges();
                    output.AppendLine(string.Format(SuccessfullyImportedMessageEntity, currentMessage.SentAt
                            .ToString("yyyy-MM-ddTHH:mm:ss",CultureInfo.InvariantCulture),
                        currentMessage.Status));
                }




            }



            return output.ToString().TrimEnd();

        } 

        public static string ImportPosts(SocialNetworkDbContext dbContext, string jsonString)
        {
            StringBuilder output = new StringBuilder();


            ImportPostsDto[]? postDtos = JsonConvert
                .DeserializeObject<ImportPostsDto[]>(jsonString);


            if (postDtos!=null&&postDtos.Length>0)
            {
                foreach (var postDto in postDtos)
                {

                    bool isDueDateValid = DateTime
                        .TryParseExact(postDto.CreatedAt, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture,
                            DateTimeStyles.None, out DateTime createdAtDate);


                    if (!IsValid(postDto)|| !isDueDateValid)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isPostExistInDb = dbContext.Posts
                        .Any(x => x.Content == postDto.Content &&
                                  x.CreatedAt == createdAtDate &&
                                  x.CreatorId == postDto.CreatorId

                        );

                    if (isPostExistInDb)
                    {
                        output.AppendLine(DuplicatedDataMessage);
                        continue;
                    }



                    Post currentPost = new Post()
                    {
                        Content = postDto.Content,
                        CreatedAt = createdAtDate,
                        CreatorId = postDto.CreatorId,
                    };



                    //	The users (Creators) associated with every post are represented by an integer ID
                    // which must match an existing record in the database. 

                    bool isPostCreatorIdIdMatchUserId = dbContext.Users
                        .Any(x => x.Id == currentPost.CreatorId);
                    if (!isPostCreatorIdIdMatchUserId)
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }


                    dbContext.Posts.Add(currentPost);
                    dbContext.SaveChanges();


                    var creatorUser = dbContext.Users
                        .First(x => x.Id == currentPost.CreatorId);

                    output.AppendLine(string.Format(SuccessfullyImportedPostEntity, creatorUser.Username,
                        currentPost.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)));

                }



            }



            return output.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            ValidationContext validationContext = new ValidationContext(dto);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (ValidationResult validationResult in validationResults)
            {
                if (validationResult.ErrorMessage != null)
                {
                    string currentMessage = validationResult.ErrorMessage;
                }
            }

            return isValid;
        }
    }
}
