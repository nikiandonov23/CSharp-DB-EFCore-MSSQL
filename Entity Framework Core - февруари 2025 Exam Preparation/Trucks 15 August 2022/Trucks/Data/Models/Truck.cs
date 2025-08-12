using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trucks.Data.Models.Enums;

namespace Trucks.Data.Models;

public class Truck
{ 
 


    [Key]
    public int Id { get; set; }



    
    [RegularExpression(@"^[A-Z]{2}[0-9]{4}[A-Z]{2}$")]
    [Required]
    public string? RegistrationNumber { get; set; }    //!!!! MOJE DA SE OKAJE DA NE E REQUIRED !!!!!



    [Required]
    [RegularExpression(@"^.{17}$")]   
    public string VinNumber { get; set; } = null!;


    [Required]
    [Range(950,1420)]
    public int TankCapacity { get; set; }



    [Required]
    [Range(5000, 29000)]
    public int CargoCapacity { get; set; }

   

    [Required]
    [Range(0,3)]
    public CategoryType CategoryType { get; set; }


    [Required]
    [Range(0,4)]
    public MakeType MakeType { get; set; }





    [ForeignKey(nameof(Despatcher))]
    [Required]
    public int DespatcherId { get; set; }
    public Despatcher Despatcher { get; set; } = null!;



    public virtual ICollection<ClientTruck> ClientsTrucks { get; set; } = new List<ClientTruck>();

}