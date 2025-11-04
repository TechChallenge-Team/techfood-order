using System;
using TechFood.Shared.Domain.Events;

namespace TechFood.Order.Domain.Events.Order;

public record class OrderCreatedEvent(
    Guid Id,
    Guid? CustomerId,
    DateTime CreatedAt) : IDomainEvent;
