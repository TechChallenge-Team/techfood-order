using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechFood.Order.Infra.Persistence.Mappings;

public class OrderMap : IEntityTypeConfiguration<Domain.Entities.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
    {
        builder.ToTable("Order");

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .IsRequired();

        builder.HasMany(o => o.Historical)
            .WithOne()
            .HasForeignKey("OrderId")
            .IsRequired();
    }
}
