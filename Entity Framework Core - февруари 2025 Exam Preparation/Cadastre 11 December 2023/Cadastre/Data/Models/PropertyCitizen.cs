using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastre.Data.Models;

public class PropertyCitizen
{
    //	PropertyId – integer, Primary Key, foreign key(required)
    //  •	Property – Property


    //	CitizenId – integer, Primary Key, foreign key(required)
    //  •	Citizen – Citizen
    [ForeignKey(nameof(Property))]
    public int PropertyId { get; set; }
    public virtual Property Property { get; set; } = null!;




    [ForeignKey(nameof(Citizen))] 
    public int CitizenId { get; set; }
    public virtual Citizen Citizen { get; set; } = null!;



}