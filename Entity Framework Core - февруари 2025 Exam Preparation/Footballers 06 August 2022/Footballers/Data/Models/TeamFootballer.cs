using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;

namespace Footballers.Data.Models;

public class TeamFootballer
{
    //TeamId – integer, Primary Key, foreign key(required)
    // •	Team – Team
    //FootballerId – integer, Primary Key, foreign key(required)
    // •	Footballer – Footballer

    [ForeignKey(nameof(Team))]
    [Required]
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;




    [ForeignKey(nameof(Footballer))]
    [Required]
    public int FootballerId { get; set; }
    public Footballer Footballer { get; set; } = null!;





}