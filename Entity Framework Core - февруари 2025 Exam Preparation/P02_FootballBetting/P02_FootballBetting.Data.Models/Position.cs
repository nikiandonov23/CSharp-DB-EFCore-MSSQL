

namespace P02_FootballBetting.Data.Models;
using System.ComponentModel.DataAnnotations;
using static P02_FootballBettingCommon.EntityValidationConstants.Position;
public class Position
{

    [Key]
    public int PositionId { get; set; }



    [Required]
    [MaxLength(PositionNameMaxLength)]
    public string Name { get; set; } = null!;



    public ICollection<Player> Players { get; set; } = new HashSet<Player>();
}