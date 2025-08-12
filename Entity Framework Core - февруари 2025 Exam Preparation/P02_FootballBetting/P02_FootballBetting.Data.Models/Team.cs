

using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;
using System.ComponentModel.DataAnnotations;
using static P02_FootballBettingCommon.EntityValidationConstants.Team;
public class Team
{

    [Key]
    public int TeamId { get; set; }


    [Required] //DEMEK NOT NULL
    [MaxLength(TeamNameMaxLength)]
    public string Name { get; set; } = null!;//trqaa da q slagam taq prostotiq kogato nqma da e NULL

    [MaxLength(TeamUrlMaxLength)]
    public string? LogoUrl { get; set; }  //Ne e zaduljitelen ;nullable 

    [Required]
    [MaxLength(TeamInitialsMaxLength)]
    public string Initials { get; set; } = null!;

    [Required]
    public decimal Budget { get; set; }






    [ForeignKey(nameof(PrimaryKitColor))]
    public int PrimaryKitColorId { get; set; }
    public virtual Color PrimaryKitColor { get; set; } = null!;




    [ForeignKey(nameof(SecondaryKitColor))]
    public int SecondaryKitColorId { get; set; }
    public virtual Color SecondaryKitColor { get; set; } = null!;




    [ForeignKey(nameof(Town))]
    public int TownId { get; set; }
     
    public virtual Town Town { get; set; } = null!;



    [InverseProperty(nameof(Game.HomeTeam))]
    public virtual ICollection<Game> HomeGames { get; set; } = new HashSet<Game>();




    [InverseProperty(nameof(Game.AwayTeam))]
    public virtual ICollection<Game> AwayGames { get; set; } = new HashSet<Game>();





    public ICollection<Player> Players { get; set; }=new HashSet<Player>();

}
