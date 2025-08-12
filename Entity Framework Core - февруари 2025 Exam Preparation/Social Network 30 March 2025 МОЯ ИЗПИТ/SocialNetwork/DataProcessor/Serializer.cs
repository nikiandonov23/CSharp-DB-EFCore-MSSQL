using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SocialNetwork.Data;
using SocialNetwork.Data.Models;
using SocialNetwork.DataProcessor.ExportDTOs;
using SocialNetwork.Utilities;

namespace SocialNetwork.DataProcessor
{
    public class Serializer
    {
        public static string ExportUsersWithFriendShipsCountAndTheirPosts(SocialNetworkDbContext dbContext)
        {
            const string xmlRoot = "Users";

            var validUsers = dbContext.Users
                //Параметъра Friendship1 не е в ExportUserPostDto-то,а в друг от ОСНОВНИТЕ класове , затова изрично го инклуудвам
                .Include(x => x.Friendships1)
                //Параметъра Friendship2 не е в ExportUserPostDto-то ,а в друг от ОСНОВНИТЕ класове  затова изрично го инклуудвам
                .Include(x => x.Friendships2)
                .ToArray()    
                .Select(x => new ExportUserPostsDto()
                {
                    //ето тук ми е НУЖЕН , затова го инклууднах по горе
                    Friendships = x.Friendships1.Count+x.Friendships2.Count,  
                    Username = x.Username,
                    Posts = x.Posts
                        .Select(x=>new ExportPostsDto()
                        {
                            Content = x.Content,
                            CreatedAt = x.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss",CultureInfo.InvariantCulture)
                            
                        })
                        .ToArray()

                })
                .OrderBy(x=>x.Username)
                .ToArray();


            string result = XmlHelper.Serialize(validUsers, xmlRoot);
            return result;

        }

        public static string ExportConversationsWithMessagesChronologically(SocialNetworkDbContext dbContext)
        {
            var validConversations = dbContext.Conversations
                .ToArray()
                .Select(x => new ExportConversationMessagesDto()
                {
                    Id=x.Id,
                    Title = x.Title,
                    StartedAt = x.StartedAt.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                    Messages = x.Messages
                        .Select(x=>new ExportMessagesDto()
                        {
                            Content = x.Content,
                            SentAt = x.SentAt.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                            Status = (int)x.Status,
                            SenderUsername = x.Sender.Username  //MOJE DA ISKA INCLUDE


                        })
                        .OrderBy(x=>x.SentAt)
                        .ToArray()

                })
                .OrderBy(x=>x.StartedAt)
                .ToArray();



            string result = JsonConvert.SerializeObject(validConversations, Formatting.Indented);
            return result;
        }
    }
}
