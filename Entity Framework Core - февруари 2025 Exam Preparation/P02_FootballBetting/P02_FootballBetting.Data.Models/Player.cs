

using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;
using System.ComponentModel.DataAnnotations;
using static P02_FootballBettingCommon.EntityValidationConstants.Player;
public class Player
{

    [Key]
    public int PlayerId { get; set; }


    [Required]
    [MaxLength(PlayerNameMaxLength)]
    public string Name { get; set; } = null!;



    [Required]
    public int SquadNumber { get; set; }



    [Required]
    public bool IsInjured { get; set; }  //дефолтната стойност на bool е false 





    [ForeignKey(nameof(Position))]
    public int PositionId { get; set; }
    public virtual Position Position { get; set; } = null!; 



    [ForeignKey(nameof(Team))]
    public int TeamId { get; set; }   

    public virtual Team Team { get; set; } = null!; //tova e zashtoto moje da ima igrach bez otbor







    //Todo Relations :   TownId 

    [ForeignKey(nameof(Town))]
    public int TownId { get; set; }

    public virtual Town Town { get; set; } = null!;


    public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; } = new HashSet<PlayerStatistic>();


}