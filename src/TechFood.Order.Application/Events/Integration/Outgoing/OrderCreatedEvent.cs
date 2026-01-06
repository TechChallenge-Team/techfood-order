using System;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Events.Integration.Outgoing;

public record OrderCreatedEvent(
    Guid OrderId
) : IIntegrationEvent;
