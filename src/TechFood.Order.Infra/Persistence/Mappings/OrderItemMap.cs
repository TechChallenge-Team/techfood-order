using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Order.Domain.Entities;

namespace TechFood.Order.Infra.Persistence.Mappings;

public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasOne<Product>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
