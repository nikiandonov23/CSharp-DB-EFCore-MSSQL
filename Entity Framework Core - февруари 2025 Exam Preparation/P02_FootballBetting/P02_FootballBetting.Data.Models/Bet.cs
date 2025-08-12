using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Data.Models.Enumerations;

namespace P02_FootballBetting.Data.Models;

public class Bet
{
    [Key]
    public int BetId { get; set; }


    [Required]
    public decimal Amount { get; set; }

    public Prediction Prediction { get; set; }  //by DEFAULT е REQUIRED


    [Required]
    public DateTime DateTime { get; set; }





    [ForeignKey(nameof(Game))]
    public int GameId { get; set; }
    public Game Game { get; set; } = null!;



    //todo UserId, 


    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;


}