using System.ComponentModel.DataAnnotations;
using Cadastre.Data.Enumerations;

namespace Cadastre.Data.Models;

public class Citizen
{
    [Key]
    public int Id { get; set; }
    

    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;


    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string LastName { get; set; } = null!;




    [Required] 
    public DateTime BirthDate { get; set; }



    [Required]
    public MaritalStatus MaritalStatus { get; set; }


    
    public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; } = new HashSet<PropertyCitizen>();

}