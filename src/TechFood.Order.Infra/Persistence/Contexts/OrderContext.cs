using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechFood.Order.Domain.Entities;
using TechFood.Shared.Domain.Enums;
using TechFood.Shared.Infra.Persistence.Contexts;

namespace TechFood.Order.Infra.Persistence.Contexts;

public class OrderContext : TechFoodContext
{
    public DbSet<Domain.Entities.Order> Orders { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);

        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties());

        var stringProperties = properties.Where(p => p.ClrType == typeof(string));
        foreach (var property in stringProperties)
        {
            var maxLength = property.GetMaxLength() ?? 50;

            property.SetColumnType($"varchar({maxLength})");
        }

        var booleanProperties = properties
            .Where(p => p.ClrType == typeof(bool) ||
                        p.ClrType == typeof(bool?));

        foreach (var property in booleanProperties)
        {
            property.SetColumnType("bit");
            property.IsNullable = false;
        }

        var dateTimeProperties = properties.Where(p => p.ClrType == typeof(DateTime));

        foreach (var property in dateTimeProperties)
        {
            property.SetColumnType("datetime");
        }

        var enumProperties = properties.Where(p => p.ClrType == typeof(Enum));

        foreach (var property in enumProperties)
        {
            property.SetColumnType("smallint");
        }

        var amountProperties = properties
            .Where(p => p.ClrType == typeof(decimal) ||
                        p.ClrType == typeof(decimal?));

        foreach (var property in amountProperties)
        {
            property.SetColumnType("decimal(6, 2)");
        }

        SeedContext(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void SeedContext(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Order>()
            .HasData(
                new
                {
                    Id = new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"),
                    Number = 1,
                    CustomerId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"),
                    CreatedAt = new DateTime(2025, 5, 13, 22, 2, 36, DateTimeKind.Utc)
            .AddTicks(6053),
                    Amount = 39.97m,
                    Status = OrderStatusType.Received,
                    Discount = 0m,
                    IsDeleted = false
                },
                new
                {
                    Id = new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"),
                    Number = 2,
                    CustomerId = new Guid("9887b301-605f-46a6-93db-ac1ce8685723"),
                    CreatedAt = new DateTime(2025, 5, 13, 22, 2, 36, DateTimeKind.Utc)
            .AddTicks(6354),
                    Amount = 26.98m,
                    Status = OrderStatusType.Ready,
                    Discount = 0m,
                    IsDeleted = false
                }
            );

        modelBuilder.Entity<OrderItem>()
            .HasData(
               new { Id = new Guid("ea31fb90-4bc3-418f-95fc-56516d5bc634"), OrderId = new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), ProductId = new Guid("090d8eb0-f514-4248-8512-cf0d61a262f0"), Quantity = 1, UnitPrice = 19.99m, IsDeleted = false },
               new { Id = new Guid("b0f1c3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), OrderId = new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), ProductId = new Guid("55f32e65-c82f-4a10-981c-cdb7b0d2715a"), Quantity = 2, UnitPrice = 9.99m, IsDeleted = false },
               new { Id = new Guid("82e5700b-c33e-40a6-bb68-7279f0509421"), OrderId = new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), ProductId = new Guid("a62dc225-416a-4e36-ba35-a2bd2bbb80f7"), Quantity = 1, UnitPrice = 21.99m, IsDeleted = false },
               new { Id = new Guid("900f65fe-47ca-4b4b-9a7c-a82c6d9c52cd"), OrderId = new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), ProductId = new Guid("86c50c81-c46e-4e79-a591-3b68c75cefda"), Quantity = 1, UnitPrice = 4.99m, IsDeleted = false }
            );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }
}
