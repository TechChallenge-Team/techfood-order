using System;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Events;

public record OrderReceivedIntegrationEvent(Guid OrderId) : IIntegrationEvent;
