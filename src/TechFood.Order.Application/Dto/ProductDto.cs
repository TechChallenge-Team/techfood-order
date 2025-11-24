using System;

namespace TechFood.Order.Application.Dto;

public record ProductDto(
    Guid Id,
    string Name,
    string ImageUrl,
    decimal Price
);
