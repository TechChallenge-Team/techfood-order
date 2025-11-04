using System;
using TechFood.Shared.Domain.Events;

namespace TechFood.Order.Domain.Events.Order;

public record class OrderCancelledEvent(
    Guid Id,
    DateTime CancelledAt) : IDomainEvent;
