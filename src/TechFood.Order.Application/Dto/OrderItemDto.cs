using System;

namespace TechFood.Order.Application.Dto;

public record OrderItemDto(Guid Id, string Name, string? ImageUrl, decimal Price, int Quantity);
