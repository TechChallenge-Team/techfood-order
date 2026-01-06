using System;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Events.Integration.Incoming;

public record PreparationStartedEvent(Guid OrderId) : IIntegrationEvent;
