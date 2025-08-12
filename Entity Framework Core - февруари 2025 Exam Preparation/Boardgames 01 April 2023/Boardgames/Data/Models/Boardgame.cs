using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using Boardgames.Data.Models.Enums;

namespace Boardgames.Data.Models;

public class Boardgame
{
    

    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    [MinLength(10)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(1, 10.00)]
    public double Rating { get; set; }

    [Required]
    [Range(2018,2023)]
    public int YearPublished { get; set; }

    [Required]
    public CategoryType CategoryType { get; set; }


    [Required]
    public string Mechanics { get; set; } = null!;

    [ForeignKey(nameof(Creator))]
    [Required]
    public int CreatorId { get; set; }

    public Creator Creator { get; set; } = null!;

    public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; } = new List<BoardgameSeller>();



}