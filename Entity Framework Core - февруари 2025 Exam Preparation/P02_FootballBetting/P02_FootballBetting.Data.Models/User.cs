
namespace P02_FootballBetting.Data.Models;

using static P02_FootballBettingCommon.EntityValidationConstants.User;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }



    [Required]
    [MaxLength(UsernameMaxLength)]
    public string Username { get; set; } = null!;



    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;


    [Required]
    [MaxLength(PasswordMaxLength)]
    public string Password { get; set; } = null!;

    [Required]
    [MaxLength(EmailMaxLength)]
    public string Email { get; set; } = null!;

    [Required]
    public decimal Balance { get; set; }



    public virtual ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
}