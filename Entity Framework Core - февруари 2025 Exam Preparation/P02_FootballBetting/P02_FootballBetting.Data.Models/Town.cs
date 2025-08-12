

using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;
using System.ComponentModel.DataAnnotations;
using static P02_FootballBettingCommon.EntityValidationConstants.Town;


public class Town
{
    [Key]
    public int TownId { get; set; }


    [Required]
    [MaxLength(TownNameMaxLength)]
    public string Name { get; set; } = null!;


    


    public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();

    






    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;



    public ICollection<Player> Players { get; set; }=new HashSet<Player>();

}