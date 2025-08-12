using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Relations_LAB.Data.Models;

namespace Relations_LAB.Data.Configurations;

public class RoomConfiguration:IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasMany(r => r.Students)
            .WithOne(r => r.Room)
            .HasForeignKey(r => r.RoomId);
    }
}