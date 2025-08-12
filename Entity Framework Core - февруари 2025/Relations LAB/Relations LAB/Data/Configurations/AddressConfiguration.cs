using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relations_LAB.Data.Models;

namespace Relations_LAB.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
            .HasOne(a => a.Student)
            .WithOne(s => s.Address)
            .HasForeignKey<Address>(s => s.StudentId);


    }
}