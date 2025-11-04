using Microsoft.Extensions.DependencyInjection;
using TechFood.Order.Application.Common.Services;
using TechFood.Order.Application.Common.Services.Interfaces;

namespace TechFood.Order.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Services
        services.AddSingleton<IOrderNumberService, OrderNumberService>();

        return services;
    }
}
