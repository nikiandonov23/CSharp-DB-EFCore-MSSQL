using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;
using Trucks.Data.Models;

namespace Trucks.DataProcessor.ImportDto;


[XmlType(nameof(Truck))]
public class ImportTrucksDto
{

    [XmlElement(nameof(RegistrationNumber))]
    [RegularExpression(@"^[A-Z]{2}[0-9]{4}[A-Z]{2}$")]
    [Required]
    public string RegistrationNumber { get; set; } = null!;




    [XmlElement(nameof(VinNumber))]
    [Required]
    [RegularExpression(@"^.{17}$")]
    public string VinNumber { get; set; } = null!;





    [XmlElement(nameof(TankCapacity))]
    [Required]
    [Range(950, 1420)]
    public int TankCapacity { get; set; }



    [XmlElement(nameof(CargoCapacity))]
    [Required]
    [Range(5000, 29000)]
    public int CargoCapacity { get; set; }



    [XmlElement(nameof(CategoryType))]
    [Required]
    [Range(0, 3)]
    public int CategoryType { get; set; }



    [XmlElement(nameof(MakeType))]
    [Required]
    [Range(0, 4)]
    public int MakeType { get; set; }

}