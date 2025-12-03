using HomeInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeInventory.Infrastructure.Persistence.Configurations;

public class WarrantyInfoConfiguration : IEntityTypeConfiguration<WarrantyInfo>
{
    public void Configure(EntityTypeBuilder<WarrantyInfo> builder)
    {
        builder.ToTable("warranty_infos");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Provider)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(w => w.SupportContact)
            .HasMaxLength(100);

        builder.Property(w => w.ExpirationDate)
            .IsRequired();

        builder.HasOne(w => w.Item)
            .WithOne(i => i.Warranty)
            .HasForeignKey<WarrantyInfo>(w => w.ItemId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}