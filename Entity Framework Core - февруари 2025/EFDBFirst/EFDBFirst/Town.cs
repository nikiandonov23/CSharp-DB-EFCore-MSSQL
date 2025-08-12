using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFDBFirst;

public partial class Town
{
    [Key]
    [Column("TownID")]
    public int TownId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Town")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
