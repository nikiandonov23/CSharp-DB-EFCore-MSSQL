using System.ComponentModel.DataAnnotations;

namespace NetPay.Data.Models;

public class Household
{
    //	Id – integer, Primary Key
    //	ContactPerson - text with length[5, 50] (required)
    //	Email – text with length[6, 80] (not required)

    //	PhoneNumber – text with length 15. (required)
    //   o   The phone number must start with a plus sign, followed by exactly three digits for the country code, a slash, exactly three digits for the area or service provider code, a dash, and exactly six digits for the subscriber number: 
    //	Example -> +144/123-123456 

    //	Use the following string for correct validation: @"^\+\d{3}/\d{3}-\d{6}$"
    //	Expenses - a collection of type Expense

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(5)]
    public string ContactPerson { get; set; } = null!;


    [MaxLength(80)]
    [MinLength(6)]
    public string? Email { get; set; }



    [Required]
    [RegularExpression(@"^\+\d{3}/\d{3}-\d{6}$")]
    public string PhoneNumber { get; set; } = null!;



    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}