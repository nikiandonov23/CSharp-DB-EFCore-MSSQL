using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetPay.Data.Models.Enums;

namespace NetPay.Data.Models;

public class Expense
{
    //	Id – integer, Primary Key
    //	ExpenseName – text with length[5, 50] (required)
    //	Amount - a decimal value in the range[0.01, 100 000](required)
    //	DueDate - DateTime(required)
    //  •	PaymentStatus – PaymentStatus enum (Paid = 1, Unpaid, Overdue, Expired) (required)
    //	HouseholdId - integer, foreign key(required)
    //  •	Household – Household
    //	ServiceId - integer, foreign key(required)
    //  •	Service - Service

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(5)]
    public string ExpenseName { get; set; } = null!;


    [Required]
    [Range(0.01, 100000)]
    public decimal Amount { get; set; }



    [Required]
    public DateTime DueDate { get; set; }


    [Required]
    public PaymentStatus PaymentStatus { get; set; }




    [ForeignKey(nameof(Household))]
    [Required]
    public int HouseholdId { get; set; }
    public Household Household { get; set; } = null!;




    [ForeignKey(nameof(Service))]
    [Required]
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;




}