using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Data.Models;

public class Friendship
{
    //	UserOneId – integer, Foreign Key(required)
    //  •	UserOne - User
    //	UserTwoId – integer, Foreign Key(required)
    //  •	UserTwo - User


    [ForeignKey(nameof(UserOne))]
    public int UserOneId { get; set; }
    public User UserOne { get; set; } = null!;





    [ForeignKey(nameof(UserTwo))]
    public int UserTwoId { get; set; }
    public User UserTwo { get; set; } = null!;

}