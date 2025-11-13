using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechFood.Order.Application;
using TechFood.Order.Application.Queries;
using TechFood.Order.Application.Services.Interfaces;
using TechFood.Order.Domain.Repositories;
using TechFood.Order.Infra.Persistence.Contexts;
using TechFood.Order.Infra.Persistence.Queries;
using TechFood.Order.Infra.Persistence.Repositories;
using TechFood.Order.Infra.Services;
using TechFood.Shared.Infra.Extensions;

namespace TechFood.Order.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddSharedInfra<OrderContext>(new InfraOptions
        {
            DbContext = (serviceProvider, dbOptions) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                dbOptions.UseSqlServer(config.GetConnectionString("DataBaseConection"));
            },
            ApplicationAssembly = typeof(DependecyInjection).Assembly
        });

        //Data
        services.AddScoped<IOrderRepository, OrderRepository>();

        //Queries
        services.AddScoped<IOrderQueryProvider, OrderQueryProvider>();

        //Services
        services.AddTechFoodClient<IBackofficeService, BackofficeService>("Backoffice");
        services.AddSingleton<IOrderNumberService, OrderNumberService>();

        return services;
    }
}
