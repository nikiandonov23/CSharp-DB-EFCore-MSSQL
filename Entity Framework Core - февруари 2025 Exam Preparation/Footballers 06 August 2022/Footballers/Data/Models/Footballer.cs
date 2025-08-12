using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Footballers.Data.Models.Enums;

namespace Footballers.Data.Models;

public class Footballer
{
    //	Id – integer, Primary Key
    //	Name – text with length[2, 40] (required)
    //	ContractStartDate – date and time(required)
    //  	ContractEndDate – date and time(required)
    //  	PositionType - enumeration of type PositionType, with possible values(Goalkeeper, Defender, Midfielder, Forward) (required)
    //	BestSkillType – enumeration of type BestSkillType, with possible values(Defence, Dribble, Pass, Shoot, Speed) (required)
    //	CoachId – integer, foreign key(required)
    //  	Coach – Coach 
    //	TeamsFootballers – collection of type TeamFootballer

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    [MinLength(2)]
    public string Name { get; set; } = null!;


    [Required]
    public DateTime ContractStartDate { get; set; }



    [Required]
    public DateTime ContractEndDate { get; set; }


    [Required]
    [Range(0,3)]
    public PositionType PositionType { get; set; }



    [Required]
    [Range(0,4)]
    public BestSkillType BestSkillType { get; set; }



    [ForeignKey(nameof(Coach))]
    public int CoachId { get; set; }
    public Coach Coach { get; set; } = null!;




    public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; } = new List<TeamFootballer>();

}