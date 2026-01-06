using System;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Events.Integration.Outgoing;

public record OrderReceivedEvent(Guid OrderId) : IIntegrationEvent;
