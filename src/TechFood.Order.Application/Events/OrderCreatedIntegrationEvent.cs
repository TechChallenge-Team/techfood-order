using System;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Events;

public record OrderCreatedIntegrationEvent(
    Guid OrderId
) : IIntegrationEvent;
