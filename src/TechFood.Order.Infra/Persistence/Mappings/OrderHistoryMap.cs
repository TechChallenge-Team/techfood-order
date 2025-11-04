using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Order.Domain.Entities;

namespace TechFood.Order.Infra.Persistence.Mappings;

public class OrderHistoryMap : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.ToTable("OrderHistory");
    }
}
