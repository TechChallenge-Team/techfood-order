using System;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Events.Integration.Incoming;

public record PaymentConfirmedEvent(Guid OrderId) : IIntegrationEvent;
