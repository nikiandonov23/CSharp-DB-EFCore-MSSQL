

using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;
using System.ComponentModel.DataAnnotations;
using static P02_FootballBettingCommon.EntityValidationConstants.Game;
public class Game
{
    [Key]
    public int GameId { get; set; }


    [Required]
    public int HomeTeamGoals { get; set; }


    [Required]
    public int AwayTeamGoals { get; set; }


    [Required]
    public decimal HomeTeamBetRate { get; set; }



    [Required]
    public decimal AwayTeamBetRate { get; set; }



    [Required]
    public decimal DrawBetRate { get; set; }



    public DateTime DateTime { get; set; }


    [MaxLength(GameResultMaxLength)]
    public string? Result { get; set; }


    //Todo HomeTeamId, AwayTeamId, 




    [ForeignKey(nameof(HomeTeam))]
    public int HomeTeamId { get; set; }
    public virtual Team HomeTeam { get; set; } = null!;




    [ForeignKey(nameof(AwayTeam))]
    public int AwayTeamId { get; set; }
    public virtual Team AwayTeam { get; set; } = null!;



    public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; } = new List<PlayerStatistic>();



    public virtual ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();





}