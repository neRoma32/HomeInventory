using HomeInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeInventory.Infrastructure.Persistence.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("items");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id); 

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Description)
            .HasMaxLength(1000);

        builder.Property(i => i.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(i => i.RoomId).IsRequired();
        builder.Property(i => i.CategoryId).IsRequired();

        builder.Property(i => i.CreatedAt);
        builder.Property(i => i.UpdatedAt);

        builder.HasOne(i => i.Room)
            .WithMany() 
            .HasForeignKey(i => i.RoomId);

        builder.HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId);

        builder.HasOne(i => i.Owner)
            .WithMany() 
            .HasForeignKey(i => i.OwnerId)
            .IsRequired();

        builder.HasMany(i => i.Tags)
            .WithMany(t => t.Items)
            .UsingEntity(j => j.ToTable("item_tags"));
    }
}