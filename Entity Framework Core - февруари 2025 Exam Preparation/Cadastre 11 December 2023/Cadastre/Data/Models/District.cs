
using System.ComponentModel.DataAnnotations;
using Cadastre.Data.Enumerations;

namespace Cadastre.Data.Models
{
    public class District
    {
        
     
        //	Properties - collection of type Property

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        [MinLength(2)]
        public string Name { get; set; } = null!;


        [Required]
        [RegularExpression(@"^[A-Z]{2}-\d{5}$")]
        public string PostalCode { get; set; } = null!;



        [Required]
        public Region Region { get; set; }


        public virtual ICollection<Property> Properties { get; set; } = new HashSet<Property>();

    }
}
