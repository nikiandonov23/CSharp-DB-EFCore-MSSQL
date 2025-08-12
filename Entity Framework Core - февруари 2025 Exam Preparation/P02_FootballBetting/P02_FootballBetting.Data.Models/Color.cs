

using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime;

namespace P02_FootballBetting.Data.Models;
using System.ComponentModel.DataAnnotations;
using static P02_FootballBettingCommon.EntityValidationConstants.Color;
public class Color
{
    [Key]
    public int ColorId { get; set; }




    [Required]
    [MaxLength(ColorNameMaxLength)]
    public string Name { get; set; } = null!;







    [InverseProperty(nameof(Team.PrimaryKitColor))]
    public virtual ICollection<Team> PrimaryKitTeams { get; set; } = new List<Team>();


    [InverseProperty(nameof(Team.SecondaryKitColor))]
    public virtual ICollection<Team> SecondaryKitTeams { get; set; } = new HashSet<Team>();

}