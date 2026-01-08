using System;

namespace TechFood.Order.Application.Dto;

public record OrderItemDto(Guid Id, Guid ProductId, decimal Price, int Quantity);
