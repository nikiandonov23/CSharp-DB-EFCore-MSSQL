using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trucks.Data.Models;

public class ClientTruck
{
    //	ClientId – integer, Primary Key, foreign key(required)
    // 	Client – Client
    //	TruckId – integer, Primary Key, foreign key(required)
    // 	Truck – Truck

    [ForeignKey(nameof(Client))]
    [Required]
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;



    [ForeignKey(nameof(Truck))]
    [Required]
    public int TruckId { get; set; }
    public Truck Truck { get; set; } = null!;
}