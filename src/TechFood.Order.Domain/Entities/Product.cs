using System;
using TechFood.Shared.Domain.Entities;

namespace TechFood.Order.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public Product(Guid id, string name, decimal price, string? imageUrl = null)
        {
            //Id = id;
            Price = price;
        }
    }
}
